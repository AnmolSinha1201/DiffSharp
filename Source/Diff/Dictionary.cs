using System;
using System.Collections.Generic;

namespace DiffSharp
{
	public static partial class DiffGenerator
	{
		public static List<Diff> SubsetDiffs(Dictionary<object, object> Dictionary1, Dictionary<object, object> Dictionary2, IEnumerable<DiffBehavior> Behavior = default)
		{
			if (Dictionary1 == null || Dictionary2 == null)
				throw new Exception("Cannot compare null kvps");

			var dictionary2Keys = Dictionary2.Keys;
			var retDiffs = new List<Diff>();

			foreach (var kvp1 in Dictionary1)
			{
				if (!dictionary2Keys.DeepFindSubset(kvp1.Key, out var key2, Behavior))
				{
					retDiffs.Add(new Diff().With(i => i.Location.Add(new KeyValuePair<object, object>(kvp1.Key, kvp1.Value))));
					continue;
				}
				
				var value1 = kvp1.Value;
				var value2 = Dictionary2[key2];

				var diffs = SubsetDiffs(value1, value2, Behavior);
				diffs.ForEach(i => i.Location.Insert(0, new KeyValuePair<object, object>(kvp1.Key, kvp1.Value)));

				if (diffs.Count != 0)
				{
					retDiffs.AddRange(diffs);
					continue;
				}
			}

			return retDiffs;
		}
	}
}