using System;


namespace DiffSharp
{
	public static partial class Helpers
	{
		public static T With<T>(this T Item, Action<T> Predicate)
		{
			Predicate(Item);
			return Item;
		}
	}
}