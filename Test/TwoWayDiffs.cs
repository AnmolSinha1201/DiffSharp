using System;
using Xunit;
using DiffSharp;

namespace Test
{
	public class TwoWayDiffTests
	{
		[Fact]
		public void TwoWayDiffs_NoDiff()
		{
			var json1 = "[{'qwe' : 'asd', 'zxc' : 123, 'tyu' : [123, 456, 789]}, {'qwe' : 'asd', 'zxc' : 456}]".Deserialize();
			var json2 = "[{'qwe' : 'asd', 'tyu' : [456, 123, 789], 'zxc' : 123}, {'qwe' : 'asd', 'zxc' : 456}]".Deserialize();

			var diffs = (json1, json2).TwoWayDiffs();
			Assert.Equal(0, diffs.Diff1.Count);
			Assert.Equal(0, diffs.Diff2.Count);
		}

		[Fact]
		public void TwoWayDiffs_Diff1()
		{
			var json1 = "[{'qwe' : 'asd', 'zxc' : 123, 'tyu' : [123, 456, 789]}, {'qwe' : 'asd', 'zxc' : 456}]".Deserialize();
			var json2 = "[{'qwe' : 'asd', 'tyu' : [456, 123, 789], 'zxc' : 123}]".Deserialize();

			var diffs = (json1, json2).TwoWayDiffs();
			Assert.Equal(1, diffs.Diff1.Count);
			Assert.Equal(0, diffs.Diff2.Count);
		}

		[Fact]
		public void TwoWayDiffs_Diff2()
		{
			var json1 = "[{'qwe' : 'asd', 'zxc' : 123, 'tyu' : [123, 456, 789]}, {'qwe' : 'asd', 'zxc' : 456}]".Deserialize();
			var json2 = "[{'qwe' : 'asd', 'tyu' : [456, 123, 789], 'zxc' : 123}]".Deserialize();

			var diffs = (json2, json1).TwoWayDiffs();
			Assert.Equal(0, diffs.Diff1.Count);
			Assert.Equal(1, diffs.Diff2.Count);
		}

		[Fact]
		public void TwoWayDiffs_BothDiffs()
		{
			var json1 = "[{'qwe' : 'asd', 'zxc' : 123, 'tyu' : [123, 345, 789]}, {'qwe' : 'asd', 'zxc' : 456}]".Deserialize();
			var json2 = "[{'qwe' : 'asd', 'zxc' : 123, 'tyu' : [123, 456, 789]}, {'qwe' : 'asd', 'zxc' : 456}]".Deserialize();

			var diffs = (json1, json2).TwoWayDiffs();
			Assert.Equal(1, diffs.Diff1.Count);
			Assert.Equal(1, diffs.Diff2.Count);
		}

		[Fact]
		public void DeepEquals_True()
		{
			var json1 = "[{'qwe' : 'asd', 'zxc' : 123, 'tyu' : [123, 456, 789]}, {'qwe' : 'asd', 'zxc' : 456}]".Deserialize();
			var json2 = "[{'qwe' : 'asd', 'zxc' : 123, 'tyu' : [123, 456, 789]}, {'qwe' : 'asd', 'zxc' : 456}]".Deserialize();

			var equals = (json1, json2).DeepEquals();
			Assert.True(equals);
		}

		[Fact]
		public void DeepEquals_False()
		{
			var json1 = "[{'qwe' : 'asd', 'zxc' : 123, 'tyu' : [123, 345, 789]}, {'qwe' : 'asd', 'zxc' : 456}]".Deserialize();
			var json2 = "[{'qwe' : 'asd', 'zxc' : 123, 'tyu' : [123, 456, 789]}, {'qwe' : 'asd', 'zxc' : 456}]".Deserialize();

			var equals = (json1, json2).DeepEquals();
			Assert.False(equals);
		}
	}
}
