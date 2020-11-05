using System;
using System.Collections.Generic;

namespace DiffSharp
{
	public static partial class DiffGenerator
	{
		public static List<Diff> SubsetDiffs(List<object> List1, List<object> List2)
		{
			if (List1 == null || List2 == null)
				throw new Exception("Cannot compare null lists");

			var retList = new List<Diff>();

			foreach (var list1Item in List1)
			{
				if (!List2.DeepFindBestMatch(list1Item, out var list2Item, out var diffs))
				{
					diffs.ForEach(i => i.Location.Insert(0, list1Item));
					retList.AddRange(diffs);
				}
			}

			return retList;
		}
	}
}