using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Common;
using System.Configuration;

namespace DataAccessOriginal
{
	public class OriginalProvider : ITestResultsProvider
	{
		private string connectionString;

		public void Initialize()
		{
			connectionString = ConfigurationManager.ConnectionStrings["NorthwindConnectionString"].ConnectionString;
		}

		public void Reset()
		{
			// nothing to do
		}

		public IList GetOrders()
		{
			List<Order> orders = new List<Order>();

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = "SELECT * FROM Orders";

				SqlCommand command = new SqlCommand(sql, connection);
				command.Connection.Open();

				using (SqlDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						Order order = FillOrder(reader);
						orders.Add(order);
					}
				}
			}

			return orders;
		}

		public IList GetCustomerProducts(string customerId)
		{
			List<Product> products = new List<Product>();

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = "SELECT p.* FROM Orders o INNER JOIN [Order Details] od ON o.OrderID = od.OrderID INNER JOIN Products p ON od.ProductID = p.ProductID WHERE o.CustomerID = @customerId";

				SqlCommand command = new SqlCommand(sql, connection);
				command.Connection.Open();
				command.Parameters.Add(new SqlParameter("@customerId", customerId));

				using (SqlDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						Product product = FillProduct(reader);
						products.Add(product);
					}
				}
			}

			return products;
		}

		public IList GetCustomerProductsLazy(string customerId)
		{
			List<Product> products = new List<Product>();

			string sql1 = "SELECT * FROM Orders WHERE CustomerID = @CustomerID";
			string sql2 = "SELECT * FROM [Order Details] WHERE OrderID = @OrderID";
			string sql3 = "SELECT * FROM Products WHERE ProductID = @ProductID";

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				// get orders
				SqlCommand command = new SqlCommand(sql1, connection);
				command.Connection.Open();
				command.Parameters.Add(new SqlParameter("@CustomerID", customerId));

				List<Order> orders = new List<Order>();

				using (SqlDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						Order order = FillOrder(reader);
						orders.Add(order);
					}
				}

				// get order details
				command.CommandText = sql2;
				List<OrderDetails> orderDetails = new List<OrderDetails>();

				foreach (Order order in orders)
				{
					command.Parameters.Clear();
					command.Parameters.Add(new SqlParameter("@OrderID", order.OrderID));

					using (SqlDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							OrderDetails details = FillOrderDetails(reader);
							orderDetails.Add(details);
						}
					}
				}

				// get products
				command.CommandText = sql3;

				foreach (OrderDetails details in orderDetails)
				{
					command.Parameters.Clear();
					command.Parameters.Add(new SqlParameter("@ProductID", details.ProductID));

					using (SqlDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							Product product = FillProduct(reader);
							products.Add(product);
						}
					}
				}
			}

			return products;
		}

		public void ModifyCustomers()
		{
			List<Customer> newCustomers = AddCustomers(10);
			EditCustomers(newCustomers);
			DeleteCustomers(newCustomers);
		}

		public void ModifyOrdersWithRelations()
		{
			List<Customer> newCustomers = AddCustomers(1);
			List<Order> newOrders = AddOrders(10);
			
			ChangeRelations(newOrders, newCustomers[0]);
			
			DeleteOrders(newOrders);
			DeleteCustomers(newCustomers);
		}

		public void ModifyCustomersBunch()
		{
			List<Customer> newCustomers = AddCustomers(1000);
			EditCustomers(newCustomers);
			DeleteCustomers(newCustomers);
		}

		public string GetName()
		{
			return "Data Reader";
		}

		private List<Customer> AddCustomers(int count)
		{
			List<Customer> customers = new List<Customer>();

			string sql = 
				"INSERT INTO Customers(CustomerID, CompanyName, ContactName, ContactTitle, Address, City, Region, PostalCode, Country, Phone, Fax)"
				+ " VALUES (@CustomerID, @CompanyName, @ContactName, @ContactTitle, @Address, @City, @Region, @PostalCode, @Country, @Phone, @Fax)";

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand command = new SqlCommand(sql, connection);
				command.Connection.Open();

				for (int i = 0; i < count; i++)
				{
					Customer customer = CreateCustomer(i);
					customers.Add(customer);

					command.Parameters.Clear();

					command.Parameters.Add(new SqlParameter("@CustomerID", customer.ID));
					command.Parameters.Add(new SqlParameter("@CompanyName", customer.CompanyName));
					command.Parameters.Add(new SqlParameter("@ContactName", customer.ContactName));
					command.Parameters.Add(new SqlParameter("@ContactTitle", customer.ContactTitle));
					command.Parameters.Add(new SqlParameter("@Address", customer.Address));
					command.Parameters.Add(new SqlParameter("@City", customer.City));
					command.Parameters.Add(new SqlParameter("@Region", customer.Region));
					command.Parameters.Add(new SqlParameter("@PostalCode", customer.PostalCode));
					command.Parameters.Add(new SqlParameter("@Country", customer.Country));
					command.Parameters.Add(new SqlParameter("@Phone", customer.Phone));
					command.Parameters.Add(new SqlParameter("@Fax", customer.Fax));

					int result = command.ExecuteNonQuery();
					if (result == 0)
						throw new ApplicationException("Customer failed to be added");
				}
			}

			return customers;
		}

		private void EditCustomers(List<Customer> customers)
		{
			string sql = "UPDATE Customers SET Address = @Address, Country = @Country WHERE CustomerID=@CustomerID";

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand command = new SqlCommand(sql, connection);
				command.Connection.Open();

				foreach (Customer customer in customers)
				{
					customer.Address = "New Address";
					customer.Country = "New Country";

					command.Parameters.Clear();

					command.Parameters.Add(new SqlParameter("@Address", customer.Address));
					command.Parameters.Add(new SqlParameter("@Country", customer.Country));
					command.Parameters.Add(new SqlParameter("@CustomerID", customer.ID));

					int result = command.ExecuteNonQuery();
					if (result == 0)
						throw new ApplicationException("Customer failed to be updated");
				}
			}
		}

		private void DeleteCustomers(List<Customer> customers)
		{
			string sql = "DELETE FROM Customers WHERE CustomerID = @CustomerID";

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand command = new SqlCommand(sql, connection);
				command.Connection.Open();

				foreach (Customer customer in customers)
				{
					command.Parameters.Clear();

					command.Parameters.Add(new SqlParameter("@CustomerID", customer.ID));

					int result = command.ExecuteNonQuery();
					if (result == 0)
						throw new ApplicationException("Customer failed to be deleted");
				}
			}
		}
        
		private Customer CreateCustomer(int i)
		{
			Customer customer = new Customer();

			customer.ID = "t" + i;
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

		private List<Order> AddOrders(int count)
		{
			List<Order> orders = new List<Order>();

			string sql =
				"INSERT INTO Orders(CustomerID, EmployeeID, OrderDate, RequiredDate, ShippedDate, ShipVia, Freight, ShipName, ShipAddress, ShipCity, ShipRegion, ShipPostalCode, ShipCountry)"
				+ " VALUES (@CustomerID, @EmployeeID, @OrderDate, @RequiredDate, @ShippedDate, @ShipVia, @Freight, @ShipName, @ShipAddress, @ShipCity, @ShipRegion, @ShipPostalCode, @ShipCountry);"
				+ "SELECT @@identity";

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand command = new SqlCommand(sql, connection);
				command.Connection.Open();

				for (int i = 0; i < count; i++)
				{
					Order order = CreateOrder();
					orders.Add(order);

					command.Parameters.Clear();

					command.Parameters.Add(new SqlParameter("@CustomerID", DBNull.Value));
					command.Parameters.Add(new SqlParameter("@EmployeeID", DBNull.Value));
					command.Parameters.Add(new SqlParameter("@OrderDate", order.OrderDate));
					command.Parameters.Add(new SqlParameter("@RequiredDate", order.RequiredDate));
					command.Parameters.Add(new SqlParameter("@ShippedDate", order.ShippedDate));
					command.Parameters.Add(new SqlParameter("@ShipVia", order.ShipVia));
					command.Parameters.Add(new SqlParameter("@Freight", order.Freight));
					command.Parameters.Add(new SqlParameter("@ShipName", order.ShipName));
					command.Parameters.Add(new SqlParameter("@ShipAddress", order.ShipAddress));
					command.Parameters.Add(new SqlParameter("@ShipCity", order.ShipCity));
					command.Parameters.Add(new SqlParameter("@ShipRegion", order.ShipRegion));
					command.Parameters.Add(new SqlParameter("@ShipPostalCode", order.ShipPostalCode));
					command.Parameters.Add(new SqlParameter("@ShipCountry", order.ShipCountry));

					order.OrderID = Convert.ToInt32(command.ExecuteScalar());
					if (order.OrderID == 0)
						throw new ApplicationException("Order failed to be added");
				}
			}

			return orders;
		}

		private Order CreateOrder()
		{
			Order order = new Order();

			order.Freight = 0;
			order.ShipVia = 1;
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
			string sql = "DELETE FROM Orders WHERE OrderID = @OrderID";

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand command = new SqlCommand(sql, connection);
				command.Connection.Open();

				foreach (Order order in orders)
				{
					command.Parameters.Clear();

					command.Parameters.Add(new SqlParameter("@OrderID", order.OrderID));

					int result = command.ExecuteNonQuery();
					if (result == 0)
						throw new ApplicationException("Order failed to be deleted");
				}
			}
		}

		private void ChangeRelations(List<Order> orders, Customer customer)
		{
			string sql = "UPDATE Orders SET CustomerID = @CustomerID WHERE OrderID = @OrderID";

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand command = new SqlCommand(sql, connection);
				command.Connection.Open();

				foreach (Order order in orders)
				{
					order.CustomerID = customer.ID;

					command.Parameters.Clear();

					command.Parameters.Add(new SqlParameter("@CustomerID", order.CustomerID));
					command.Parameters.Add(new SqlParameter("@OrderID", order.OrderID));

					int result = command.ExecuteNonQuery();
					if (result == 0)
						throw new ApplicationException("Order failed to be updated");
				}
			}
		}

		private DateTime GetDate()
		{
			return new DateTime(2009, 1, 20);
		}

		#region Helper methods

		private Product FillProduct(SqlDataReader reader)
		{
			Product product = new Product();
			product.ProductID = reader.GetInt32(reader.GetOrdinal("ProductID"));
			product.SupplierID = reader.GetInt32(reader.GetOrdinal("SupplierID"));
			product.CategoryID = reader.GetInt32(reader.GetOrdinal("CategoryID"));
			product.ProductName = GetSafeStringValue(reader, "ProductName");
			product.QuantityPerUnit = GetSafeStringValue(reader, "QuantityPerUnit");
			product.UnitPrice = reader.GetDecimal(reader.GetOrdinal("UnitPrice"));
			product.UnitsOnOrder = GetSafeIntegerValue(reader, "UnitsOnOrder");
			product.UnitsInStock = GetSafeIntegerValue(reader, "UnitsInStock");
			product.ReorderLevel = GetSafeIntegerValue(reader, "ReorderLevel");
			product.Discontinued = reader.GetBoolean(reader.GetOrdinal("Discontinued"));

			return product;
		}

		private OrderDetails FillOrderDetails(SqlDataReader reader)
		{
			OrderDetails details = new OrderDetails();
			details.OrderID = reader.GetInt32(reader.GetOrdinal("OrderID"));
			details.ProductID = reader.GetInt32(reader.GetOrdinal("ProductID"));
			details.UnitPrice = reader.GetDecimal(reader.GetOrdinal("UnitPrice"));
			details.Quantity = reader.GetInt16(reader.GetOrdinal("Quantity"));
			details.Discount = reader.GetFloat(reader.GetOrdinal("Discount"));

			return details;
		}

		private Order FillOrder(SqlDataReader reader)
		{
			Order order = new Order();
			order.OrderID = reader.GetInt32(reader.GetOrdinal("OrderID"));
			order.CustomerID = reader.GetString(reader.GetOrdinal("CustomerID"));
			order.EmployeeID = reader.GetInt32(reader.GetOrdinal("EmployeeID"));
			order.OrderDate = GetSafeDateTimeValue(reader, "OrderDate");
			order.RequiredDate = GetSafeDateTimeValue(reader, "RequiredDate");
			order.ShippedDate = GetSafeDateTimeValue(reader, "ShippedDate");
			order.ShipVia = reader.GetInt32(reader.GetOrdinal("ShipVia"));
			order.Freight = reader.GetDecimal(reader.GetOrdinal("Freight"));
			order.ShipName = GetSafeStringValue(reader, "ShipName");
			order.ShipAddress = GetSafeStringValue(reader, "ShipAddress");
			order.ShipCity = GetSafeStringValue(reader, "ShipCity");
			order.ShipRegion = GetSafeStringValue(reader, "ShipRegion");
			order.ShipPostalCode = GetSafeStringValue(reader, "ShipPostalCode");
			order.ShipCountry = GetSafeStringValue(reader, "ShipCountry");

			return order;
		}

		private string GetSafeStringValue(SqlDataReader reader, string columnName)
		{
			SqlString sqlString = reader.GetSqlString(reader.GetOrdinal(columnName));
			return sqlString.IsNull ? null : sqlString.Value;
		}

		private DateTime? GetSafeDateTimeValue(SqlDataReader reader, string columnName)
		{
			SqlDateTime sqlDateTime = reader.GetSqlDateTime(reader.GetOrdinal(columnName));
			return sqlDateTime.IsNull ? (DateTime?)null : sqlDateTime.Value;
		}

		private int? GetSafeIntegerValue(SqlDataReader reader, string columnName)
		{
			SqlInt16 sqlInt16 = reader.GetSqlInt16(reader.GetOrdinal(columnName));
			return sqlInt16.IsNull ? (int?)null : sqlInt16.Value;
		}

		#endregion
	}
}
