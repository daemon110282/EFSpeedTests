using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Castle.ActiveRecord;

namespace DataAccessActiveRecord
{
	[ActiveRecord("Customers")]
	public class Customer : ActiveRecordBase<Customer>
	{
		IList orders = new List<Order>();

		[PrimaryKey(Column = "CustomerID", Generator = PrimaryKeyType.Assigned)]
		public string ID { get; set; }

		[Property()]
		public string Address { get; set; }

		[Property()]
		public string City { get; set; }

		[Property()]
		public string CompanyName { get; set; }

		[Property()]
		public string ContactName { get; set; }

		[Property()]
		public string ContactTitle { get; set; }

		[Property()]
		public string Country { get; set; }

		[Property()]
		public string Fax { get; set; }

		[Property()]
		public string Phone { get; set; }

		[Property()]
		public string PostalCode { get; set; }

		[Property()]
		public string Region { get; set; }

		public IList Orders
		{
			get { return orders; }
			set { orders = value; }
		}

		public override bool Equals(object obj)
		{
			if (obj is Customer)
			{
				bool ret = false;
				Customer objComp = (Customer)obj;
				ret = (objComp.ID == this.ID);
				return ret;
			}
			else
			{
				return false;
			}
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public static Customer FindById(int id)
		{
			return (Customer)FindByPrimaryKey(typeof(Customer), id);
		}
	}
}
