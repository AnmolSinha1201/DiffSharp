using System;
using Xunit;
using DiffSharp;

namespace Test
{
	public class SubsetTests
	{
		[Fact]
		public void StringComparison()
		{
			var json1 = "['qwe', 'asd', 'zxc']";
			var json2 = "['qwe', 'asd', 'zxc']";

			var diffs = DiffGenerator.SubsetDiffs(json1, json2);
			Assert.Equal(0, (int)diffs.Count);
		}

		[Fact]
		public void ListComparison()
		{
			var json1 = "['asd', 'qwe', 'zxc']".Deserialize();
			var json2 = "['qwe', 'asd', 'zxc']".Deserialize();

			var diffs = DiffGenerator.SubsetDiffs(json1, json2);
			Assert.Equal(0, (int)diffs.Count);
		}

		[Fact]
		public void DictionaryComparison()
		{
			var json1 = "{'qwe' : 'asd', 'zxc' : 123}".Deserialize();
			var json2 = "{'zxc' : 123, 'qwe' : 'asd'}".Deserialize();

			var diffs = DiffGenerator.SubsetDiffs(json1, json2);
			Assert.Equal(0, (int)diffs.Count);
		}

		[Fact]
		public void ArrayComparison()
		{
			var json1 = new[] { 123, 456, 789 };
			var json2 = new[] { 456, 123, 789 };

			var diffs = DiffGenerator.SubsetDiffs(json1, json2);
			Assert.Equal(0, (int)diffs.Count);
		}

		[Fact]
		public void ListOfDictionaryComparison_DictionaryPartialMatch()
		{
			var json1 = "[{'qwe' : 'asd', 'zxc' : 123}, {'qwe' : 'asd', 'zxc' : 456}]".Deserialize();
			var json2 = "[{'qwe' : 'asd', 'zxc' : 456}, {'qwe' : 'asd', 'zxc' : 123}]".Deserialize();

			var diffs = DiffGenerator.SubsetDiffs(json1, json2);
			Assert.Equal(0, (int)diffs.Count);
		}

		[Fact]
		public void ListOfDictionaryComparison_DictionaryMissing()
		{
			var json1 = "[{'qwe' : 'asd', 'zxc' : 123}, {'qwe' : 'asd', 'zxc' : 456}]".Deserialize();
			var json2 = "[{'qwe' : 'asd', 'zxc' : 456}]".Deserialize();

			var diffs = DiffGenerator.SubsetDiffs(json1, json2);
			Assert.Equal(1, (int)diffs.Count);
		}

		[Fact]
		public void ListOfDictionaryWithListComparison()
		{
			var json1 = "[{'qwe' : 'asd', 'zxc' : 123, 'tyu' : [123, 456, 789]}, {'qwe' : 'asd', 'zxc' : 456}]".Deserialize();
			var json2 = "[{'qwe' : 'asd', 'tyu' : [456, 123, 789], 'zxc' : 123}, {'qwe' : 'asd', 'zxc' : 456}]".Deserialize();

			var diffs = DiffGenerator.SubsetDiffs(json1, json2);
			Assert.Equal(0, (int)diffs.Count);
		}

		[Fact]
		public void NullInListComparison()
		{
			var json1 = "[123, null, 789]".Deserialize();
			var json2 = "[123, 789, null]".Deserialize();

			var diffs = DiffGenerator.SubsetDiffs(json1, json2);
			Assert.Equal(0, (int)diffs.Count);
		}
	}
}
