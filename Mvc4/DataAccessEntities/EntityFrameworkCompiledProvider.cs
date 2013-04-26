using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Text;
using System.Collections;
using System.Diagnostics;

namespace DataAccessEntities
{
	public class EntityFrameworkCompiledProvider : EntityFrameworkProvider
	{
		public class CustomerWithOrders
		{
			public IEnumerable<OrderWithProducts> Orders;
		}

		public class OrderWithProducts
		{
			public IEnumerable<Product> Products;
		}

		protected static Func<NorthwindEntities, IQueryable<Order>> compiledGetOrders =
			CompiledQuery.Compile(
				(NorthwindEntities ne) =>
					(from o in ne.Orders
					 select o));

		protected static Func<NorthwindEntities, string, IQueryable<EntityCollection<Order>>> compiledGetCustomerProductsComplex =
			CompiledQuery.Compile(
				(NorthwindEntities ne, string customerId) =>
					(from cust in ne.Customers
					 where cust.CustomerID == customerId
					 select cust.Orders));

		protected static Func<NorthwindEntities, string, IQueryable<CustomerWithOrders>> compiledGetCustomerProducts =
			CompiledQuery.Compile(
				(NorthwindEntities ne, string customerId) =>
					(from cust in ne.Customers
					 where cust.CustomerID == customerId
					 select new CustomerWithOrders()
					 {
						 Orders =
							from ord in cust.Orders
							select new OrderWithProducts()
							{
								Products =
								   from det in ord.Order_Details
								   select det.Product
							}
					 }));

		public EntityFrameworkCompiledProvider(bool withTracking) : base(withTracking)
		{
		}

		public override IList GetOrders()
		{
			return compiledGetOrders(entities).ToList();
		}

		public override IList GetCustomerProducts(string customerId)
		{
			var allProductsData =
				compiledGetCustomerProducts(entities, customerId).ToList();

            Debug.Print(((ObjectQuery)compiledGetCustomerProducts(entities, customerId)).ToTraceString());

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

		public override IList GetCustomerProductsLazy(string customerId)
		{
			var ordersColl =
				compiledGetCustomerProductsComplex(entities, customerId).ToList();
            
            Debug.Print(((ObjectQuery)compiledGetCustomerProductsComplex(entities, customerId)).ToTraceString());

			List<Product> products = new List<Product>();
			foreach (EntityCollection<Order> orders in ordersColl)
			{
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
			}

			return products;
		}

		public override string GetName()
		{
			return "Entity Framework (compiled queries)";
		}
	}
}
