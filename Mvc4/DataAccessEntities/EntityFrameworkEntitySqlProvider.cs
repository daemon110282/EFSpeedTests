using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using Common;

namespace DataAccessEntities
{
	public class EntityFrameworkEntitySqlProvider : EntityFrameworkProvider
	{
		public class CustomerWithOrders
		{
			public IEnumerable<OrderWithProducts> Orders;
		}

		public class OrderWithProducts
		{
			public IEnumerable<Product> Products;
		}

		public EntityFrameworkEntitySqlProvider(bool withTracking) : base(withTracking)
		{
		}

		public override IList GetOrders()
		{
			string query = "NorthwindEntities.Orders";
			List<Order> orders = entities.CreateQuery<Order>(query).ToList();

			return orders;
		}

		public override IList GetCustomerProducts(string customerId)
		{
			string query = "SELECT VALUE p FROM NorthwindEntities.Products AS p"
				+ " JOIN NorthwindEntities.Order_Details AS od ON od.ProductID == p.ProductID"
				+ " JOIN NorthwindEntities.Orders AS o ON o.OrderID == od.OrderID"
				+ " WHERE o.Customers.CustomerID == @customerId";
			var products = entities.CreateQuery<Product>(query, new[] { new ObjectParameter("customerId", customerId) }).ToList();

			return products;
		}

		public override IList GetCustomerProductsLazy(string customerId)
		{
			string query = "SELECT VALUE o FROM NorthwindEntities.Orders AS o WHERE o.Customers.CustomerID == @customerId";
			//string query = "SELECT VALUE c.Orders FROM NorthwindEntities.Customers AS c WHERE c.CustomerID == @customerId";
			var orders = entities.CreateQuery<Order>(query, new [] {new ObjectParameter("customerId", customerId)}).ToList();
			
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

		public override string GetName()
		{
			return "Entity Framework (EntitySQL)";
		}
	}
}
