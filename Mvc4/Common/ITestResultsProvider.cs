using System.Collections;

namespace Common
{
	public interface ITestResultsProvider
	{
		void Initialize();
		void Reset();

		IList GetOrders();
		IList GetCustomerProducts(string customerId);
		IList GetCustomerProductsLazy(string customerId);

		void ModifyCustomers();
		void ModifyOrdersWithRelations();
		void ModifyCustomersBunch();

		string GetName();
	}
}