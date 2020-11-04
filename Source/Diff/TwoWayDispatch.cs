using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace DiffSharp
{
	public static partial class DiffGenerator
	{
		public static (List<Diff> Diff1, List<Diff> Diff2) TwoWayDiffs(this (object Object1, object Object2) Objects)
		=> TwoWayDiffs(Objects.Object1, Objects.Object2);

		public static (List<Diff> Diff1, List<Diff> Diff2) TwoWayDiffs(object Object1, object Object2)
		{
			var diff1 = SubsetDiffs(Object1, Object2);
			var diff2 = SubsetDiffs(Object2, Object1);

			return (diff1, diff2);
		}


		public static bool DeepEquals(this (object Object1, object Object2) Objects)
		=> DeepEquals(Objects.Object1, Objects.Object2);

		public static bool DeepEquals(object Object1, object Object2)
		{
			var diffs = TwoWayDiffs(Object1, Object2);
			return diffs.Diff1.Count == 0 && diffs.Diff2.Count == 0;
		}
	}
}