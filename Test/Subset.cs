using System;
using Xunit;
using DiffSharp;

namespace Test
{
	public class UnitTest1
	{
		[Fact]
		public void Test1()
		{
			var json1 = "['qwe', 'asd', 'zxc']";
			var json2 = "['qwe', 'asd', 'zxc']";

			var diffs = DiffGenerator.SubsetDiffs(json1, json2);
			Assert.Equal(0, diffs.Count);
		}
	}
}
