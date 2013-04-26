using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;

namespace DataAccessActiveRecord
{
	[ActiveRecord("Orders")]
	public class Order : ActiveRecordBase<Order>
	{
		[PrimaryKey(Column = "OrderID", Generator = PrimaryKeyType.Identity)]
		public int ID { get; set; }

		[Property("CustomerID")]
		public string CustomerID { get; set; }

		[Property()]
		public int? EmployeeID { get; set; }

		[Property()]
		public DateTime? OrderDate { get; set; }

		[Property()]
		public DateTime? RequiredDate { get; set; }

		[Property()]
		public DateTime? ShippedDate { get; set; }

		[Property()]
		public int? ShipVia { get; set; }

		[Property()]
		public float? Freight { get; set; }

		[Property()]
		public string ShipName { get; set; }

		[Property()]
		public string ShipAddress { get; set; }

		[Property()]
		public string ShipCity { get; set; }

		[Property()]
		public string ShipRegion { get; set; }

		[Property()]
		public string ShipPostalCode { get; set; }

		[Property()]
		public string ShipCountry { get; set; }
	}
}
