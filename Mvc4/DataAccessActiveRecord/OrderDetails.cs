using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;
using NHibernate.Criterion;
//using NHibernate..Expression;

namespace DataAccessActiveRecord
{
	[ActiveRecord("`Order Details`")]
	public class OrderDetails : ActiveRecordBase<OrderDetails>
	{
		[PrimaryKey(PrimaryKeyType.Native, "OrderID")]
		public int ID { get; set; }

		[BelongsTo("OrderID")]
		public Order Order { get; set; }

		[BelongsTo("ProductID")]
		public Product Product { get; set; }

		[Property("UnitPrice")]
		public float UnitPrice { get; set; }

		[Property("Quantity")]
		public int Quantity { get; set; }

		[Property("Discount")]
		public float Discount { get; set; }

		public static OrderDetails[] FindByOrder(Order o)
		{
			return ((OrderDetails[])(FindAll(typeof(OrderDetails), Expression.Eq("Order.ID", o.ID))));
		}
	}
}
