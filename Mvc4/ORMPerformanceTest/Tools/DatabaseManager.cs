using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace ORMPerformanceTest.Tools
{
	public class DatabaseManager
	{
		private string connectionString;

		public DatabaseManager()
		{
			connectionString = ConfigurationManager.ConnectionStrings["NorthwindConnectionString"].ConnectionString;
		}

		public void RefreshDatabase()
		{
			string script = ReadDatabaseScript();
			if (String.IsNullOrEmpty(script))
				throw new ApplicationException("Script is empty");

			string[] delimitedSqlCommand = 
				Regex.Split(script, @"\s+GO\s+", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();

				// Creates the db objects
				foreach (string sqlCommand in delimitedSqlCommand)
				{
					if (string.IsNullOrEmpty(sqlCommand))
						continue;

					SqlCommand command = new SqlCommand(sqlCommand, connection);
					command.CommandType = CommandType.Text;
					command.ExecuteNonQuery();
				}
			}
		}

		private string ReadDatabaseScript()
		{
			string dbFile = ConfigurationManager.AppSettings["TestDBClearFile"] ?? null;

			if (dbFile == null)
				throw new ApplicationException("Path to the test database was not found in the configuration file");

			string assemblyPath = Assembly.GetAssembly(typeof (DatabaseManager)).Location;
			if (assemblyPath == null)
				throw new ApplicationException("Path to the assembly was not found");

			try
			{
				StreamReader reader = File.OpenText(Path.Combine(Path.GetDirectoryName(assemblyPath), dbFile));
				return reader.ReadToEnd();
			}
			catch (Exception ex)
			{
				throw new ApplicationException("Clear cache database script can't be found or read", ex);
			}

			return null;
		}
	}
}
