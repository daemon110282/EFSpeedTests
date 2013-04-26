using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;

namespace DataAccessActiveRecord
{
	[ActiveRecord("Products")]
	public class Product : ActiveRecordBase<Product>
	{
		[PrimaryKey("ProductID")]
		public int ID { get; set; }

		[Property("ProductName")]
		public string Name { get; set; }

		[Property("UnitPrice")]
		public decimal Price { get; set; }

		[Property("SupplierId")]
		public int SupplierId { get; set; }

		public static Product FindById(int id)
		{
			return (Product)FindByPrimaryKey(typeof(Product), id);
		}
	}
}
