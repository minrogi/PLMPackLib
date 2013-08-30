#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace Pic
{
	namespace Logging
	{
		public struct Priority
		{
			public const int Lowest = 0;
			public const int Low = 1;
			public const int Normal = 2;
			public const int High = 3;
			public const int Highest = 4;
		}
		public struct Category
		{
			public const string General = "General";
			public const string Trace = "Trace";
		}
	}
}
