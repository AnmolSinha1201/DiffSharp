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

		[Fact]
		public void PrimitivesAsString()
		{
			var json1 = "[123, 456, 789]".Deserialize();
			var json2 = "['123', '456', '789']".Deserialize();
			var behavior = new[] { DiffBehavior.PrimitivesAsString };

			var diffs = (json1, json2).TwoWayDiffs(behavior);
			Assert.Equal(0, diffs.Diff1.Count);
			Assert.Equal(0, diffs.Diff2.Count);
		}

		[Fact]
		public void AllowRepeat_NotDeclared()
		{
			var json1 = "[123, 456, 123]".Deserialize();
			var json2 = "[123, 456, 789]".Deserialize();

			var diffs = (json1, json2).TwoWayDiffs();
			Assert.Equal(1, diffs.Diff1.Count);
			Assert.Equal(1, diffs.Diff2.Count);
		}

		[Fact]
		public void AllowRepeat()
		{
			var json1 = "[123, 456, 123]".Deserialize();
			var json2 = "[123, 456]".Deserialize();
			var behavior = new[] { DiffBehavior.AllowRepeat };

			var diffs = (json1, json2).TwoWayDiffs(behavior);
			Assert.Equal(0, diffs.Diff1.Count);
			Assert.Equal(0, diffs.Diff2.Count);
		}
	}
}
