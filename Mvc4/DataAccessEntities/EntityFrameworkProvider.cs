using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Data.Entity;
using System.Text;
using Common;

namespace DataAccessEntities
{
	public class EntityFrameworkProvider : ITestResultsProvider
	{
		protected NorthwindEntities entities;
		protected bool withTracking;

		public EntityFrameworkProvider(bool withTracking)
		{
			this.withTracking = withTracking;
		}

		public virtual void Initialize()
		{
			entities = new NorthwindEntities();
			if (!withTracking)
			{
				entities.Orders.MergeOption = MergeOption.NoTracking;
				entities.Customers.MergeOption = MergeOption.NoTracking;
			}
		}

		public virtual void Reset()
		{
			if (entities != null)
				entities.Dispose();

			Initialize();
		}

		public virtual IList GetOrders()
		{
			List<Order> orders =
				(from o in entities.Orders
				 select o).ToList();

			return orders;
		}

		public virtual IList GetCustomerProducts(string customerId)
		{
			var allProductsData = 
				(from cust in entities.Customers
				where cust.CustomerID == customerId
				select new
					{Orders = 
						from ord in cust.Orders
				        select new
							{Products = 
								from det in ord.Order_Details
				        	    select det.Product}}).ToList();

			List<Product> products = new List<Product>();
			var productsData = allProductsData.FirstOrDefault();
			if (productsData != null)
			foreach (var ordersData in productsData.Orders)
			{
				foreach (Product product in ordersData.Products)
				{
					products.Add(product);
				}
			}

			return products;
		}

		public virtual IList GetCustomerProductsLazy(string customerId)
		{
			var orders =
				(from cust in entities.Customers
				 where cust.CustomerID == customerId
				 select cust.Orders).FirstOrDefault();

			if (orders == null)
				return new List<Product>();

			List<Product> products = new List<Product>();
			foreach (Order order in orders)
			{
				if (!order.Order_Details.IsLoaded)
					order.Order_Details.Load();
				foreach (Order_Details details in order.Order_Details)
				{
					if (!details.ProductReference.IsLoaded)
						details.ProductReference.Load();
					products.Add(details.Product);
				}
			}

			return products;
		}

		public virtual void ModifyCustomers()
		{
			List<Customer> newCustomers = AddCustomers(10);
			entities.SaveChanges();

			EditCustomers(newCustomers);
			entities.SaveChanges();

			DeleteCustomers(newCustomers);
			entities.SaveChanges();
		}

		public virtual void ModifyOrdersWithRelations()
		{
			List<Customer> newCustomers = AddCustomers(1);
			List<Order> newOrders = AddOrders(10);
			entities.SaveChanges();

			ChangeRelations(newOrders, newCustomers[0]);
			entities.SaveChanges();

			DeleteCustomers(newCustomers);
			DeleteOrders(newOrders);
			entities.SaveChanges();
		}

		public virtual void ModifyCustomersBunch()
		{
			List<Customer> newCustomers = AddCustomers(1000);
			entities.SaveChanges();

			EditCustomers(newCustomers);
			entities.SaveChanges();

			DeleteCustomers(newCustomers);
			entities.SaveChanges();
		}

		public virtual string GetName()
		{
			return "Entity Framework";
		}

		private List<Customer> AddCustomers(int count)
		{
			List<Customer> customers = new List<Customer>();

			for (int i = 0; i < count; i++)
			{
				Customer customer = CreateCustomer(i);

				entities.AddToCustomers(customer);
				customers.Add(customer);
			}

			return customers;
		}

		private Customer CreateCustomer(int number)
		{
			Customer customer = new Customer();

			customer.CustomerID = "t" + number;
			customer.Address = "Address";
			customer.City = "City";
			customer.CompanyName = "Company";
			customer.ContactName = "Contact Name";
			customer.ContactTitle = "Contact Title";
			customer.Country = "Country";
			customer.Fax = "Fax";
			customer.Phone = "Phone";
			customer.PostalCode = "Postal";
			customer.Region = "Region";

			return customer;
		}

		private void EditCustomers(List<Customer> customers)
		{
			foreach (Customer customer in customers)
			{
				customer.Address = "New Address";
				customer.Country = "New Country";
			}
		}

		private void DeleteCustomers(List<Customer> customers)
		{
			foreach (Customer customer in customers)
			{
				entities.DeleteObject(customer);
			}
		}

		private List<Order> AddOrders(int count)
		{
			List<Order> orders = new List<Order>();

			for (int i = 0; i < count; i++)
			{
				Order order = CreateOrder();

				entities.AddToOrders(order);
				orders.Add(order);
			}

			return orders;
		}

		private Order CreateOrder()
		{
			Order order = new Order();

			Shipper shipper =
				(from s in entities.Shippers
				 where s.ShipperID == 1
				 select s).FirstOrDefault();

			order.Freight = 0;
			order.Shippers = shipper;
			order.OrderDate = GetDate();
			order.RequiredDate = GetDate();
			order.ShipAddress = "Ship Address";
			order.ShipCity = "Ship City";
			order.ShipCountry = "Ship Country";
			order.ShipName = "Ship Name";
			order.ShippedDate = GetDate();
			order.ShipPostalCode = "Postal";
			order.ShipRegion = "Ship Region";

			return order;
		}

		private void DeleteOrders(List<Order> orders)
		{
			foreach (Order order in orders)
			{
				entities.DeleteObject(order);
			}
		}

		private void ChangeRelations(List<Order> orders, Customer customer)
		{
			foreach (Order order in orders)
			{
				customer.Orders.Add(order);
			}
		}

		private DateTime GetDate()
		{
			return new DateTime(2009, 1, 20);
		}
	}
}