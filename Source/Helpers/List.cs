using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DiffSharp
{
	public static partial class Helpers
	{
		public static List<T> CreateList<T>(this T Item)
		=> new List<T>() { Item };

		public static string Join(this IEnumerable Enumerable, string Delimiter)
		=> string.Join(Delimiter, Enumerable);
	}
}