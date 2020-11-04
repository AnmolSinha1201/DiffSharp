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
				var list2Item = List2.DeepFindSubset(list1Item);
				if (list2Item == null)
				{
					var diff = new Diff();
					diff.Location.Add(list1Item);

					retList.Add(diff);
				}
			}

			return retList;
		}
	}
}