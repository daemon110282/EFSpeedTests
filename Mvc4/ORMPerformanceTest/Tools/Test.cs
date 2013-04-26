using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORMPerformanceTest.Tools
{
	public enum TestType
	{
		Initialization,
		Selection,
		Modification
	}

	public enum TestKind
	{
		Initialize,
		SelectSimple,
		SelectWithRelationsLazy,
		SelectWithRelationsOptimal,
		ModifySimple,
		ModifyWithRelations,
		ModifyHeavy
	}

	public class Test
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public TestType Type { get; set; }
		public TestKind Kind { get; set; }

		public Test(int id, string name, TestType type, TestKind kind)
		{
			Id = id;
			Name = name;
			Type = type;
			Kind = kind;
		}
	}
}