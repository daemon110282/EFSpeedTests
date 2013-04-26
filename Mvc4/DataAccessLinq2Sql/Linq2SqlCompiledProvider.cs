using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;

namespace DataAccessLinq2Sql
{
	public class Linq2SqlCompiledProvider : Linq2SqlProvider
	{
		public class CustomerWithOrders
		{
			public IEnumerable<OrderWithProducts> Orders;
		}

		public class OrderWithProducts
		{
			public IEnumerable<Product> Products;
		}

		protected static Func<NorthwindClassesDataContext, IQueryable<Order>> compiledGetOrders =
			CompiledQuery.Compile(
				(NorthwindClassesDataContext ne) =>
					(from o in ne.Orders
					 select o));

		protected static Func<NorthwindClassesDataContext, string, IQueryable<EntitySet<Order>>> compiledGetCustomerProductsComplex =
			CompiledQuery.Compile(
				(NorthwindClassesDataContext ne, string customerId) =>
					(from cust in ne.Customers
					 where cust.CustomerID == customerId
					 select cust.Orders));

		protected static Func<NorthwindClassesDataContext, string, IQueryable<CustomerWithOrders>> compiledGetCustomerProducts =
			CompiledQuery.Compile(
				(NorthwindClassesDataContext ne, string customerId) =>
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

		public Linq2SqlCompiledProvider(bool withTracking) : base(withTracking)
		{
		}

		public override IList GetOrders()
		{
			return compiledGetOrders(context).ToList();
		}

		public override IList GetCustomerProducts(string customerId)
		{
			var allProductsData =
				compiledGetCustomerProducts(context, customerId).ToList();

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
			var orders =
				compiledGetCustomerProductsComplex(context, customerId).FirstOrDefault();

			if (orders == null)
				return new List<Product>();

			List<Product> products = new List<Product>();
			foreach (Order order in orders)
			{
				foreach (Order_Detail details in order.Order_Details)
				{
					products.Add(details.Product);
				}
			}

			return products;
		}

		public override string GetName()
		{
			return "LINQ to SQL (compiled queries)";
		}
	}
}
