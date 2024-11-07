using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extension
{
	public static class OrderState
	{		
		public const string Processing = "Processing";
		public const string Waiting = "Waiting";
		public const string Complete = "Complete";
		public const string Canceled = "Canceled";		
	}
}
