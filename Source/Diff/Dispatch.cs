using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DiffSharp
{
	public static partial class DiffGenerator
	{
		private static Type[] doubleTypeList = new [] { typeof(double), typeof(decimal), typeof(float) };

		public static List<Diff> SubsetDiffs(object Object1, object Object2)
		{
			if (Object1 is IDictionary d1 && Object2 is IDictionary d2)
				return SubsetDiffs(Object1.Map<Dictionary<object, object>>(), Object2.Map<Dictionary<object, object>>());
			if (Object1 is IList l1 && Object2 is IList l2)
				return SubsetDiffs(l1.Cast<object>().ToList(), l2.Cast<object>().ToList());
				

			if (doubleTypeList.Contains(Object1.GetType()) && doubleTypeList.Contains(Object2.GetType()))
			{
				if (Math.Abs(Convert.ToDouble(Object1) - Convert.ToDouble(Object2)) > double.Epsilon)
					return new Diff().With(i => i.Location.Add(Object1)).CreateList();
			}
			if (Object1.GetType().IsValueType && Object2.GetType().IsValueType)
			{
				if (!Object1.Equals(Object2))
					return new Diff().With(i => i.Location.Add(Object1)).CreateList();
			}

			// Two proper objects compared as KVPs
			return SubsetDiffs(Object1.Map<Dictionary<object, object>>(), Object2.Map<Dictionary<object, object>>());
		}
	}

	public class Diff
	{
		public List<object> Location = new List<object>();
	}
}