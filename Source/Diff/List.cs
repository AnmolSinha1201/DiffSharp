using System;
using System.Collections.Generic;

namespace DiffSharp
{
	public static partial class DiffGenerator
	{
		public static List<Diff> SubsetDiffs(List<object> List1, List<object> List2, IEnumerable<DiffBehavior> Behavior = null)
		{
			if (List1 == null || List2 == null)
				throw new Exception("Cannot compare null lists");

			var retList = new List<Diff>();

			for (int index = 0; index < List1.Count; index++)
			{
				var list1Item = List1[index];
				if (!List2.DeepFindBestMatch(list1Item, out var list2Item, out var diffs, Behavior))
				{
					diffs.ForEach(i => i.Location.Insert(0, new KeyValuePair<object, object>(index, list1Item)));
					retList.AddRange(diffs);
				}
			}

			return retList;
		}
	}
}