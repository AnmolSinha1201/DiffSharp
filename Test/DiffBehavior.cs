using System;
using Xunit;
using DiffSharp;

namespace Test
{
	public class DiffBehaviorTests
	{
		[Fact]
		public void IgnoreCase()
		{
			var json1 = "[{'qwe' : 'asd', 'zxc' : 123, 'tyu' : [123, 456, 789]}, {'qwe' : 'asd', 'zxc' : 456}]".Deserialize();
			var json2 = "[{'qwe' : 'asd', 'zxc' : 123, 'tyu' : [123, 456, 789]}, {'qwe' : 'asd', 'zxc' : 456}]".ToUpper().Deserialize();
			var behavior = new [] { DiffBehavior.IgnoreCase };

			var diffs = (json1, json2).TwoWayDiffs(behavior);
			Assert.Equal(0, diffs.Diff1.Count);
			Assert.Equal(0, diffs.Diff2.Count);
		}
	}
}
