using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;

namespace DataAccessActiveRecord
{
	[ActiveRecord("Suppliers")]
	public class Supplier : ActiveRecordBase<Supplier>
	{
		[PrimaryKey("SupplierID")]
		public int ID { get; set; }

		[Property("CompanyName")]
		public string Name { get; set; }

		/// <summary>
		/// Returns the Suppliers ordered by Name
		/// </summary>
		/// <returns>Suppliers array</returns>
		/*public new static Supplier[] FindAll()
		{
			return (Supplier[])FindAll(typeof(Supplier), new Order[] { Order.Asc("Name") });
		}*/

		public static Supplier FindById(int id)
		{
			return (Supplier)FindByPrimaryKey(typeof(Supplier), id);
		}
	}
}
