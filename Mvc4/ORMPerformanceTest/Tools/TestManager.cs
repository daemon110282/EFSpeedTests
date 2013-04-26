using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORMPerformanceTest.Tools
{
	public class TestManager
	{
		private List<Test> tests;

		public TestManager()
		{
			tests = new List<Test>();

			tests.Add(new Test(1, "Simple selection (Orders)", TestType.Selection, TestKind.SelectSimple));
			tests.Add(new Test(2, "Advanced selection (Products by Customer) - lazy", TestType.Selection, TestKind.SelectWithRelationsLazy));
			tests.Add(new Test(3, "Advanced selection (Products by Customer) - optimal", TestType.Selection, TestKind.SelectWithRelationsOptimal));

			tests.Add(new Test(11, "Simple modification", TestType.Modification, TestKind.ModifySimple));
			tests.Add(new Test(12, "Advanced modification (with relations)", TestType.Modification, TestKind.ModifyWithRelations));
			tests.Add(new Test(13, "Heavy modification (many entities)", TestType.Modification, TestKind.ModifyHeavy));
		}

		public List<Test> GetSelectionTests()
		{
			return tests.Where(t => t.Type == TestType.Selection).ToList();
		}

		public List<Test> GetModificationTests()
		{
			return tests.Where(t => t.Type == TestType.Modification).ToList();
		}
	}
}
