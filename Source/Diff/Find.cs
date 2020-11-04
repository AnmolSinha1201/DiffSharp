using System.Collections;
using System.Collections.Generic;

namespace DiffSharp
{
	public static partial class DiffGenerator
	{
		public static object DeepFindSubset(this IEnumerable Enumerable, object Target)
		{
			foreach (var item in Enumerable)
			{
				var diffs = SubsetDiffs(Target, item);
				if (diffs.Count != 0)
					continue;

				// var reverseDiffs = GenerateDiffs(item, Target);
				// if (reverseDiffs.Count != 0)
				// 	continue;

				return item;
			}

			return null;
		}
	}
}