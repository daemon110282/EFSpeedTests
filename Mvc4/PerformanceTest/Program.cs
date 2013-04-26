using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using DataAccessActiveRecord;
using DataAccessEntities;
using DataAccessLinq2Sql;
using DataAccessOriginal;

namespace DataAccessPerformanceTest
{
	internal enum TestType
	{
		Orders,
		OrdersMultiple,
		CustomerProducts,
		CustomerProductsMultiple,
		CustomerProductsComplex,
		CustomerProductsComplexMultiple,
		CustomerProductsMixed,
		CustomerModify
	}

	class Program
	{
		static void Main(string[] args)
		{
            //http://merle-amber.blogspot.ru/2008/11/net-orm-1.html

            ITestResultsProvider provider = null;
            if (args.Length == 0) return;
            bool useTracking = args.Length>1 && bool.TryParse(args[1], out useTracking) ? useTracking : true;
            switch (args[0])
            {
                case "ado":
                    provider = new OriginalProvider();
                    break;
                case "linq": 
                    provider = new Linq2SqlProvider(useTracking);
                    break;
                case "ef":
                    provider = new EntityFrameworkProvider(useTracking);
                    break;
                case "ar":
                    provider = null; //new ActiveRecordProvider(); 
                    break;
                default:
                    break;
            }
            if (provider == null) return;

			TestInitialization(provider);
            Console.Out.WriteLine(string.Format("Use tracking: {0}", useTracking.ToString()));
			DateTime testBegin = DateTime.Now;

			// TODO: Uncomment necessary test or few of them
			//RunTest(provider, TestType.Orders);
			RunTest(provider, TestType.OrdersMultiple);
			//RunTest(provider, TestType.CustomerProducts);
			RunTest(provider, TestType.CustomerProductsMultiple);
			//RunTest(provider, TestType.CustomerProductsComplex);
			RunTest(provider, TestType.CustomerProductsComplexMultiple);
			//RunTest(provider, TestType.CustomerProductsMixed);
			//RunTest(provider, TestType.CustomerModify);

			TimeSpan passedTime = DateTime.Now - testBegin;

			Console.Out.WriteLine("Overall spent time: " + passedTime.TotalMilliseconds);
            //Console.ReadKey();
		}

		private static void RunTest(ITestResultsProvider provider, TestType testType)
		{
			switch (testType)
			{
				case TestType.Orders:
					TestOrders(provider);
					break;
				case TestType.OrdersMultiple:
					TestOrdersMultipleCalls(provider);
					break;
				case TestType.CustomerProducts:
					TestCustomerProducts(provider);
					break;
				case TestType.CustomerProductsMultiple:
					TestCustomerProductsMultipleCalls(provider);
					break;
				case TestType.CustomerProductsComplex:
					TestCustomerProductsComplex(provider);
					break;
				case TestType.CustomerProductsComplexMultiple:
					TestCustomerProductsComplexMultipleCalls(provider);
					break;
				case TestType.CustomerProductsMixed:
					TestCustomerProductsMixedCalls(provider);
					break;
				case TestType.CustomerModify:
					TestCustomerModify(provider);
					break;
			}
		}

		private static void TestInitialization(ITestResultsProvider provider)
		{
			DateTime testBegin = DateTime.Now;
			provider.Initialize();
			TimeSpan passedTime = DateTime.Now - testBegin;

			Console.Out.WriteLine(string.Format("Initialization {1} spent time: {0}", passedTime.TotalMilliseconds, provider.GetName()));
		}

		public static void TestOrders(ITestResultsProvider resultsProvider)
		{
			DateTime testBegin = DateTime.Now;
			IList orders = resultsProvider.GetOrders();
			TimeSpan passedTime = DateTime.Now - testBegin;

			Console.Out.WriteLine("Orders result: " + orders.Count);
			Console.Out.WriteLine("Orders spent time: " + passedTime.TotalMilliseconds);
		}

		public static void TestOrdersMultipleCalls(ITestResultsProvider resultsProvider)
		{
			DateTime testBegin = DateTime.Now;
			IList orders = resultsProvider.GetOrders();
			orders = resultsProvider.GetOrders();
			orders = resultsProvider.GetOrders();
			TimeSpan passedTime = DateTime.Now - testBegin;

			Console.Out.WriteLine("Orders result: " + orders.Count);
			Console.Out.WriteLine("Orders spent time: " + passedTime.TotalMilliseconds);
		}

		public static void TestCustomerProducts(ITestResultsProvider resultsProvider)
		{
			DateTime testBegin = DateTime.Now;
			IList products = resultsProvider.GetCustomerProducts("BERGS");
			TimeSpan passedTime = DateTime.Now - testBegin;

			Console.Out.WriteLine("CustomerProducts result: " + products.Count);
			Console.Out.WriteLine("CustomerProducts spent time: " + passedTime.TotalMilliseconds);
		}

		public static void TestCustomerProductsMultipleCalls(ITestResultsProvider resultsProvider)
		{
			DateTime testBegin = DateTime.Now;
			IList products = resultsProvider.GetCustomerProducts("BERGS");
			products = resultsProvider.GetCustomerProducts("BERGS");
			products = resultsProvider.GetCustomerProducts("BERGS");
			TimeSpan passedTime = DateTime.Now - testBegin;

			Console.Out.WriteLine("CustomerProducts result: " + products.Count);
			Console.Out.WriteLine("CustomerProducts spent time: " + passedTime.TotalMilliseconds);
		}

		public static void TestCustomerProductsComplex(ITestResultsProvider resultsProvider)
		{
			DateTime testBegin = DateTime.Now;
			IList products = resultsProvider.GetCustomerProductsLazy("BERGS");
			TimeSpan passedTime = DateTime.Now - testBegin;

			Console.Out.WriteLine("CustomerProductsComplex result: " + products.Count);
			Console.Out.WriteLine("CustomerProductsComplex spent time: " + passedTime.TotalMilliseconds);
		}

		public static void TestCustomerProductsComplexMultipleCalls(ITestResultsProvider resultsProvider)
		{
			DateTime testBegin = DateTime.Now;
			IList products = resultsProvider.GetCustomerProductsLazy("BERGS");
			products = resultsProvider.GetCustomerProductsLazy("BERGS");
			products = resultsProvider.GetCustomerProductsLazy("BERGS");
			TimeSpan passedTime = DateTime.Now - testBegin;

			Console.Out.WriteLine("CustomerProductsComplex result: " + products.Count);
			Console.Out.WriteLine("CustomerProductsComplex spent time: " + passedTime.TotalMilliseconds);
		}

		public static void TestCustomerProductsMixedCalls(ITestResultsProvider resultsProvider)
		{
			DateTime testBegin = DateTime.Now;
			IList orders = resultsProvider.GetOrders();
			IList products = resultsProvider.GetCustomerProducts("BERGS");
			TimeSpan passedTime = DateTime.Now - testBegin;

			Console.Out.WriteLine("CustomerProductsMixed result: " + products.Count);
			Console.Out.WriteLine("CustomerProductsMixed spent time: " + passedTime.TotalMilliseconds);
		}

		public static void TestCustomerModify(ITestResultsProvider resultsProvider)
		{
			DateTime testBegin = DateTime.Now;
			resultsProvider.ModifyCustomers();
			TimeSpan passedTime = DateTime.Now - testBegin;

			Console.Out.WriteLine("CustomerModify spent time: " + passedTime.TotalMilliseconds);
		}
	}
}
