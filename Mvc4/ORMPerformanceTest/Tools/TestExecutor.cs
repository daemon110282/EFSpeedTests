using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Common;

namespace ORMPerformanceTest.Tools
{
	public class TestExecutor
	{
		private Test test;
		private ITestResultsProvider resultsProvider;
		private int executionNumber;
		private bool contextCaching;
		private bool tracking;

		public TestExecutor(Test test, ITestResultsProvider resultsProvider, int executionNumber, bool tracking, bool contextCaching) 
			: this(test, resultsProvider, executionNumber, tracking, contextCaching, false)
		{
		}

		public TestExecutor(Test test, ITestResultsProvider resultsProvider, int executionNumber, 
			bool tracking, bool contextCaching, bool deferredInitialization)
		{
			this.test = test;
			this.resultsProvider = resultsProvider;
			this.executionNumber = executionNumber;
			this.contextCaching = contextCaching;
			this.tracking = tracking;
			if (!deferredInitialization)
				resultsProvider.Initialize();
		}

		public PerformanceResults Initialize()
		{
			PerformanceResults perfResults = new PerformanceResults() { Test = test };

			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();

			resultsProvider.Initialize();

			stopwatch.Stop();
			perfResults.AvgTimeCost = perfResults.TotalTimeCost = (double)stopwatch.ElapsedMilliseconds / executionNumber;

			return perfResults;
		}

		public PerformanceResults Execute()
		{
			DatabaseManager dbManager = new DatabaseManager();

			IList results = null;
			PerformanceResults perfResults = new PerformanceResults() { Test = test };
			perfResults.Modificators[TestModificator.Tracking] = tracking;
			perfResults.Modificators[TestModificator.FirstLevelCaching] = contextCaching;

			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();

			for (int i = 0; i < executionNumber; i++)
			{
				stopwatch.Stop();
				dbManager.RefreshDatabase();
				if (!contextCaching)
					resultsProvider.Reset();
				stopwatch.Start();

				switch (test.Kind)
				{
					case TestKind.SelectSimple:
						results = resultsProvider.GetOrders();
						perfResults.ItemsSelected = results.Count;
						break;
					case TestKind.SelectWithRelationsLazy:
						results = resultsProvider.GetCustomerProductsLazy("BERGS");
						perfResults.ItemsSelected = results.Count;
						break;
					case TestKind.SelectWithRelationsOptimal:
						results = resultsProvider.GetCustomerProducts("BERGS");
						perfResults.ItemsSelected = results.Count;
						break;
					case TestKind.ModifySimple:
						resultsProvider.ModifyCustomers();
						perfResults.ItemsSelected = 0;
						break;
					case TestKind.ModifyWithRelations:
						resultsProvider.ModifyOrdersWithRelations();
						perfResults.ItemsSelected = 0;
						break;
					case TestKind.ModifyHeavy:
						resultsProvider.ModifyCustomersBunch();
						perfResults.ItemsSelected = 0;
						break;
				}
			}

			stopwatch.Stop();

			perfResults.AvgTimeCost = (double)stopwatch.ElapsedMilliseconds / executionNumber;
			perfResults.TotalTimeCost = stopwatch.ElapsedMilliseconds;

			return perfResults;
		}
	}
}
