using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORMPerformanceTest.Tools
{
	public enum TestModificator
	{
		Tracking,
		FirstLevelCaching
		//SecondLevelCaching
	}

	public class PerformanceResults
	{
		public Test Test { get; set; }
		public Dictionary<TestModificator, bool> Modificators;
		public double AvgTimeCost { get; set; }
		public double TotalTimeCost { get; set; }
		public int ItemsSelected { get; set; }

		public PerformanceResults()
		{
			Modificators = new Dictionary<TestModificator, bool>();

			Modificators.Add(TestModificator.Tracking, false);
			Modificators.Add(TestModificator.FirstLevelCaching, false);
			//Modificators.Add(TestModificator.SecondLevelCaching, false);
		}
	}
}
