using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common;
using DataAccessEntities;
using DataAccessLinq2Sql;
using DataAccessOriginal;

namespace ORMPerformanceTest.Tools
{
	public class ResultsProviderFactory
	{
		private const int TOOL_DATA_READER = 0;
		private const int TOOL_LINQ_2_SQL = 1;
		private const int TOOL_LINQ_2_SQL_COMPILED = 2;
		private const int TOOL_ENTITY_FRAMEWORK = 3;
		private const int TOOL_ENTITY_FRAMEWORK_COMPILED = 4;
		private const int TOOL_ENTITY_FRAMEWORK_ENTITYSQL = 5;

		public ITestResultsProvider CreateProvider(int providerId, bool withTracking)
		{
			ITestResultsProvider resultsProvider = null;
			switch (providerId)
			{
				case TOOL_DATA_READER:
					resultsProvider = new OriginalProvider();
					break;
				case TOOL_LINQ_2_SQL:
					resultsProvider = new Linq2SqlProvider(withTracking);
					break;
				case TOOL_LINQ_2_SQL_COMPILED:
					resultsProvider = new Linq2SqlCompiledProvider(withTracking);
					break;
				case TOOL_ENTITY_FRAMEWORK:
					resultsProvider = new EntityFrameworkProvider(withTracking);
					break;
				case TOOL_ENTITY_FRAMEWORK_COMPILED:
					resultsProvider = new EntityFrameworkCompiledProvider(withTracking);
					break;
				case TOOL_ENTITY_FRAMEWORK_ENTITYSQL:
					resultsProvider = new EntityFrameworkEntitySqlProvider(withTracking);
					break;
			}

			return resultsProvider;
		}

		public Dictionary<int, ITestResultsProvider> GetAllProviders()
		{
			Dictionary<int, ITestResultsProvider> providers = new Dictionary<int, ITestResultsProvider>();

			providers.Add(TOOL_DATA_READER, new OriginalProvider());
			providers.Add(TOOL_LINQ_2_SQL, new Linq2SqlProvider(false));
			providers.Add(TOOL_LINQ_2_SQL_COMPILED, new Linq2SqlCompiledProvider(false));
			providers.Add(TOOL_ENTITY_FRAMEWORK, new EntityFrameworkProvider(false));
			providers.Add(TOOL_ENTITY_FRAMEWORK_COMPILED, new EntityFrameworkCompiledProvider(false));
			providers.Add(TOOL_ENTITY_FRAMEWORK_ENTITYSQL, new EntityFrameworkEntitySqlProvider(false));

			return providers;
		}
	}
}
