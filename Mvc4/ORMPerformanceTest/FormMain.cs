using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common;
using DataAccessEntities;
using DataAccessLinq2Sql;
using DataAccessOriginal;
using ORMPerformanceTest.Tools;

namespace ORMPerformanceTest
{
	public partial class FormMain : Form
	{
		public List<PerformanceResults> resultsList;

		public FormMain()
		{
			InitializeComponent();
		}

		private void FormMain_Load(object sender, EventArgs e)
		{
			TestManager testManager = new TestManager();

			cboxTestSelect.DataSource = testManager.GetSelectionTests();
			cboxTestSelect.ValueMember = "Id";
			cboxTestSelect.DisplayMember = "Name";

			cboxTestModify.DataSource = testManager.GetModificationTests();
			cboxTestModify.ValueMember = "Id";
			cboxTestModify.DisplayMember = "Name";

			resultsList = new List<PerformanceResults>();

			if (cboxTool.Items.Count > 0)
				cboxTool.SelectedIndex = 0;

			Dictionary<int, ITestResultsProvider> resultsProviders = new ResultsProviderFactory().GetAllProviders();

			InitTools(resultsProviders);

			RunInitializationTest(resultsProviders.Values.ToList());
		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void btnRun_Click(object sender, EventArgs e)
		{
			if (cboxTool.SelectedIndex < 0)
			{
				MessageBox.Show("Please select the item in the list of data access tools");
				return;
			}

			int? executionNumber = GetExecutionNumber();
			if (executionNumber == null || executionNumber < 1)
			{
				MessageBox.Show("Please input the positive integer number as the execution number");
				return;
			}

			ITestResultsProvider resultsProvider = 
				new ResultsProviderFactory().CreateProvider(cboxTool.SelectedIndex, checkTracking.Checked);
			if (resultsProvider == null)
			{
				MessageBox.Show("Unknown item in the data access tool list");
				return;
			}

			Test selectedTest = null;
			switch (tabControl.SelectedIndex)
			{
				case 0:
					selectedTest = (Test) cboxTestSelect.SelectedItem;
					break;
				case 1:
					selectedTest = (Test) cboxTestModify.SelectedItem;
					break;
			}

			TestExecutor executor = 
				new TestExecutor(selectedTest, resultsProvider, executionNumber.Value, checkTracking.Checked, checkContextCaching.Checked);
			PerformanceResults results = executor.Execute();

			WriteIntoLog(resultsProvider, results);
		}

		private int? GetExecutionNumber()
		{
			int number;
			if (int.TryParse(txtExecutionNumber.Text, out number))
				return number;
			return null;
		}

		private void InitTools(Dictionary<int, ITestResultsProvider> providers)
		{
			Dictionary<int, string> bindableTools = new Dictionary<int, string>();
			foreach (int key in providers.Keys)
			{
				bindableTools.Add(key, providers[key].GetName());
			}

			cboxTool.ValueMember = "Key";
			cboxTool.DisplayMember = "Value";
			cboxTool.DataSource = new BindingSource(bindableTools, null);
		}

		private void RunInitializationTest(List<ITestResultsProvider> resultsProviders)
		{
			foreach (ITestResultsProvider resultsProvider in resultsProviders)
			{
				TestExecutor executor = new TestExecutor(new Test(0, "Initialize", TestType.Initialization, TestKind.Initialize), 
					resultsProvider, 1, false, false, true);
				PerformanceResults results = executor.Initialize();

				WriteIntoLog(resultsProvider, results);
			}
		}

		private void WriteIntoLog(ITestResultsProvider resultsProvider, PerformanceResults results)
		{
			gridViewResults.Rows.Insert(0, gridViewResults.Rows.Count + 1,
				resultsProvider.GetName() + ": " + results.Test.Name, GenerateModificatorsReport(results),
				results.AvgTimeCost.ToString(), results.TotalTimeCost.ToString());
		}

		private string GenerateModificatorsReport(PerformanceResults results)
		{
			StringBuilder builder = new StringBuilder();

			foreach (TestModificator modificator in results.Modificators.Keys)
			{
				builder.Append(modificator);
				builder.Append(" ");
				builder.Append(results.Modificators[modificator] ? "ON" : "OFF");

				if (results.Modificators.Keys.Last() != modificator)
					builder.Append(" , ");
			}

			return builder.ToString();
		}
	}
}
