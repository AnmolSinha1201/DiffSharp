using System;
using Xunit;
using DiffSharp;

namespace Test
{
	public class UnitTest1
	{
		[Fact]
		public void StringComparison()
		{
			var json1 = "['qwe', 'asd', 'zxc']";
			var json2 = "['qwe', 'asd', 'zxc']";

			var diffs = DiffGenerator.SubsetDiffs(json1, json2);
			Assert.Equal(0, diffs.Count);
		}

		[Fact]
		public void ListComparison()
		{
			var json1 = "['asd', 'qwe', 'zxc']".Deserialize();
			var json2 = "['qwe', 'asd', 'zxc']".Deserialize();

			var diffs = DiffGenerator.SubsetDiffs(json1, json2);
			Assert.Equal(0, diffs.Count);
		}

		[Fact]
		public void DictionaryComparison()
		{
			var json1 = "{'qwe' : 'asd', 'zxc' : 123}".Deserialize();
			var json2 = "{'zxc' : 123, 'qwe' : 'asd'}".Deserialize();

			var diffs = DiffGenerator.SubsetDiffs(json1, json2);
			Assert.Equal(0, diffs.Count);
		}
	}
}
