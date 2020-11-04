using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace DiffSharp
{
	public static partial class DiffGenerator
	{
		private static Type[] doubleTypeList = new [] { typeof(double), typeof(decimal), typeof(float) };

		public static List<Diff> SubsetDiffs(object Object1, object Object2)
		{
			if (Object1 is JObject || Object2 is JObject)
			{
				Object1 = Object1.Map<Dictionary<object, object>>();
				Object2 = Object2.Map<Dictionary<object, object>>();
			}
			// JArrays are IList
			if (Object1 is JValue j1 && Object2 is JValue j2)
			{
				Object1 = j1.Value;
				Object2 = j2.Value;
			}

			if (Object1 is IDictionary d1 && Object2 is IDictionary d2)
				return SubsetDiffs(Object1.Map<Dictionary<object, object>>(), Object2.Map<Dictionary<object, object>>());
			if (Object1 is IList l1 && Object2 is IList l2)
				return SubsetDiffs(l1.Cast<object>().ToList(), l2.Cast<object>().ToList());
				

			var retList = new List<Diff>();
			if (doubleTypeList.Contains(Object1.GetType()) && doubleTypeList.Contains(Object2.GetType()))
			{
				if (Math.Abs(Convert.ToDouble(Object1) - Convert.ToDouble(Object2)) > double.Epsilon)
					retList.Add(new Diff().With(i => i.Location.Add(Object1)));

				return retList;
			}
			if (Object1.GetType().IsValueType && Object2.GetType().IsValueType)
			{
				if (!Object1.Equals(Object2))
					retList.Add(new Diff().With(i => i.Location.Add(Object1)));

				return retList;
			}
			if (Object1.GetType() == typeof(string) && Object2.GetType() == typeof(string))
			{
				if (!Object1.Equals(Object2))
					retList.Add(new Diff().With(i => i.Location.Add(Object1)));

				return retList;
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