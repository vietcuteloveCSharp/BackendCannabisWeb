using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enum.Domain
{
	public static class TechnicalSpecs
	{
		public enum EPowerSupplyType
		{
			AC = 0,
			DC = 1,
			Battery = 2
		}

		public enum ECoolingType
		{
			Active = 0,  // Có quạt
			Passive = 1  // Tản nhiệt nhôm tự nhiên
		}
	}
}
