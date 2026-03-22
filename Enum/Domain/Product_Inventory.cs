using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enum.Domain
	{ 
	public static class Product_Inventory
	{
		public enum EProductType
		{
			Growlight = 0,
			Seed = 1,
			Growtent = 2,
			CarbonFilter = 3,
			Nutrient = 4,
			Dehumidifier = 5
		}
		public enum EPowerSypplyType
		{
			AC =1,
			TA=2

		}
		public enum EStrainType
		{
			Indica = 0,
			Sativa = 1,
			Hybrid = 2
		}

		public enum EDifficulty
		{
			Easy = 0,
			Medium = 1,
			Hard = 2
		}

		public enum ESpectrumType
		{
			FullSpectrum = 0,
			Veg = 1,
			Bloom = 2,
			UV = 3,
			IR = 4
		}
	}
}
