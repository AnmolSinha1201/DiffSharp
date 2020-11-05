using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace DiffSharp
{
	public class Diff
	{
		public List<KeyValuePair<object, object>> Location = new List<KeyValuePair<object, object>>();
	}

	public enum DiffBehavior
	{
		AllowRepeat, IgnoreCase, ValueTypesAsString
	}
}