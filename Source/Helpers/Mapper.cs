using System;
using Newtonsoft.Json;

namespace DiffSharp
{
	public static partial class Helpers
	{
		public static T Map<T>(this object Source)
		=> (T)Source.Map(typeof(T));

		public static object Map(this object Source, Type ReturnType)
		{
			var json = JsonConvert.SerializeObject(Source);
			var retVal = JsonConvert.DeserializeObject(json, ReturnType);

			return retVal;
		}
	}
}