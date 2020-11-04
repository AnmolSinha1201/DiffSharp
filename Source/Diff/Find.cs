using System.Collections;
using System.Collections.Generic;

namespace DiffSharp
{
	public static partial class DiffGenerator
	{
		public static bool DeepFindSubset(this IEnumerable Enumerable, object Target, out object FoundObject)
		{
			foreach (var item in Enumerable)
			{
				var diffs = SubsetDiffs(Target, item);
				if (diffs.Count != 0)
					continue;

				FoundObject = item;
				return true;
			}

			FoundObject = null;
			return false;
		}
	}
}