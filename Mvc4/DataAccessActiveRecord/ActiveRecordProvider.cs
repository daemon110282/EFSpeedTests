using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework.Config;
using Castle.ActiveRecord.Queries;
using Common;
using NHibernate;
using NHibernate.Cfg;

namespace DataAccessActiveRecord
{
	public class ActiveRecordProvider : ITestResultsProvider
	{
		public void Initialize()
		{
            //ActiveRecordStarter.Initialize(typeof(BlogPost).Assembly, source);
            //ActiveRecordStarter.CreateSchema();

            /*
			Hashtable properties = new Hashtable();

			properties.Add("hibernate.connection.driver_class", "NHibernate.Driver.SqlClientDriver");
			properties.Add("hibernate.dialect", "NHibernate.Dialect.MsSql2008Dialect");
			properties.Add("hibernate.connection.provider", "NHibernate.Connection.DriverConnectionProvider");
			properties.Add("hibernate.connection.connection_string", "Data Source=.\\SQLexpress;Initial Catalog=Northwind;Integrated Security=True");
              InPlaceConfigurationSource source = new InPlaceConfigurationSource();
            source.Add(typeof(ActiveRecordBase), (IDictionary<string, string>)properties);
            */

            XmlConfigurationSource source = new XmlConfigurationSource("config.xml");
			ActiveRecordStarter.Initialize(source, typeof(Order), typeof(Product), typeof(Supplier), typeof(Customer), typeof(OrderDetails));
		}

		public virtual void Reset()
		{
			// is not tested
			Initialize();
		}

		public IList GetOrders()
		{
			List<Order> orders = new List<Order>(Order.FindAll());
			return orders;
		}

		public IList GetCustomerProducts(string customerId)
		{
			string query = "SELECT p FROM OrderDetails od INNER JOIN od.Order o, od.Product p where p.ID = od.Product.ID AND od.Order.ID = o.ID AND o.CustomerID = :customerId";
			SimpleQuery<Product> q = new SimpleQuery<Product>(query);

			q.SetParameter("customerId", customerId);

			List<Product> products = new List<Product>(q.Execute());
			return products;
		}

		public IList GetCustomerProductsLazy(string customerId)
		{
			List<Product> products = new List<Product>();

			using (new SessionScope())
			{
				//Customer customer = Customer.Find(customerId);

				List<Order> orders = new List<Order>(Order.FindAllByProperty("CustomerID", customerId));
				foreach (Order order in orders)
				{
					List<OrderDetails> orderDetails = new List<OrderDetails>(OrderDetails.FindByOrder(order));
					foreach (OrderDetails orderDetail in orderDetails)
					{
						//Product product = Product.Find(orderDetail.Product);
						products.Add(orderDetail.Product);
					}
				}
			}

			return products;
		}

		public void ModifyCustomers()
		{
			List<Customer> newCustomers = AddCustomers(10);
			//EditCustomers(newCustomers);
			//DeleteCustomers(newCustomers);
		}

		public void ModifyOrdersWithRelations()
		{
		}

		public void ModifyCustomersBunch()
		{
			List<Customer> newCustomers = AddCustomers(1000);
			//EditCustomers(newCustomers);
			//DeleteCustomers(newCustomers);
		}

		public string GetName()
		{
			return "ActiveRecord";
		}

		private List<Customer> AddCustomers(int count)
		{
			Configuration cfg = new Configuration();
			//cfg.AddAssembly("NHibernate.Examples");

			ISessionFactory factory = cfg.BuildSessionFactory();
			ISession session = factory.OpenSession();
			ITransaction transaction = session.BeginTransaction();

            List<Customer> customers = new List<Customer>();

			for (int i = 0; i < count; i++)
			{
				Customer customer = CreateCustomer(i);

				session.Save(customer);
				customers.Add(customer);
			}

			transaction.Commit();
			session.Close();

			return customers;
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

		/*private void EditCustomers(List<Customer> customers)
		{
			foreach (Customer customer in customers)
			{
				customer.Address = "New Address";
				customer.Country = "New Country";
			}
			entities.SaveChanges();
		}

		private void DeleteCustomers(List<Customer> customers)
		{
			foreach (Customer customer in customers)
			{
				entities.DeleteObject(customer);
			}
			entities.SaveChanges();
		}*/
	}
}
