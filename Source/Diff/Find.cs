using System.Collections;
using System.Collections.Generic;

namespace DiffSharp
{
	public static partial class DiffGenerator
	{
		public static bool DeepFindSubset(this IEnumerable Enumerable, object Target, out object FoundObject, IEnumerable<DiffBehavior> Behavior = default)
		{
			foreach (var item in Enumerable)
			{
				var diffs = SubsetDiffs(Target, item, Behavior);
				if (diffs.Count != 0)
					continue;

				FoundObject = item;
				return true;
			}

			FoundObject = null;
			return false;
		}

		public static bool DeepFindBestMatch(this IEnumerable Enumerable, object Target, out object FoundObject, out List<Diff> Diffs, IEnumerable<DiffBehavior> Behavior = default)
		{
			object bestMatch = null;
			List<Diff> bestDiffs = null;

			foreach (var item in Enumerable)
			{
				var diffs = SubsetDiffs(Target, item, Behavior);
				if (bestMatch == null || bestDiffs.Count > diffs.Count)
				{
					bestMatch = item;
					bestDiffs = diffs;
				}
				if (diffs.Count == 0)
					break;
			}

			FoundObject = bestMatch;
			Diffs = bestDiffs;
			return bestDiffs.Count == 0;
		}
	}
}