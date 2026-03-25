namespace DAl.Data
{
	public class DbInitializer
	{
		public static void Seed(ModelBuilder modelBuilder)
		{
			// 1. Seed ChipModel
			modelBuilder.Entity<ChipModel>().HasData(
				new ChipModel { ChipModelId = 1, Manufacturer = "Samsung", ModelChip = "LM301H", ModelName = "Evo", Efficiency = 3.10m, Description = "Top tier for horticulture" },
				new ChipModel { ChipModelId = 2, Manufacturer = "Osram", ModelChip = "GH CSSRM4.24", ModelName = "Oslon Square", Efficiency = 4.00m, Description = "Hyper Red 660nm" },
				new ChipModel { ChipModelId = 3, Manufacturer = "Cree", ModelChip = "JK2835", ModelName = "J Series", Efficiency = 2.80m, Description = "Cost-effective solution" },
				new ChipModel { ChipModelId = 4, Manufacturer = "Bridgelux", ModelChip = "BXEB-L0340", ModelName = "Vero 29", Efficiency = 2.60m, Description = "High power COB" },
				new ChipModel { ChipModelId = 5, Manufacturer = "Seoul", ModelChip = "MJT-3030", ModelName = "SunLike", Efficiency = 2.75m, Description = "Full spectrum natural light" }
			);

			// 2. Seed CoolingSystem
			modelBuilder.Entity<CoolingSystem>().HasData(
				new CoolingSystem { CoolingSystemId = 1, Type = ECoolingType.Fan, Description = "Aluminium Heatsink" },
				new CoolingSystem { CoolingSystemId = 2, Type = ECoolingType.WaterCooling, Description = "Dual Ball Bearing Fan" },
				new CoolingSystem { CoolingSystemId = 3, Type = ECoolingType.AirConditioning, Description = "Water cooling block" },
				new CoolingSystem { CoolingSystemId = 4, Type = ECoolingType.Fan, Description = "Smart PWM Fan" },
				new CoolingSystem { CoolingSystemId = 5, Type = ECoolingType.AirConditioning, Description = "Graphene Coating" }
			);

			// 3. Seed Spectrum
			modelBuilder.Entity<Spectrum>().HasData(
				new Spectrum { SpectrumId = 1, Type = ESpectrumType.FullSpectrum, ColorHexCode = "#FFFFFF", SpectrumChartUrl = "https://cdn.example.com/s1.png", Description = "Balanced growth" },
				new Spectrum { SpectrumId = 2, Type = ESpectrumType.Flowering, ColorHexCode = "#FF5733", SpectrumChartUrl = "https://cdn.example.com/s2.png", Description = "Flowering stage boost" },
				new Spectrum { SpectrumId = 3, Type = ESpectrumType.Vegetative, ColorHexCode = "#33FF57", SpectrumChartUrl = "https://cdn.example.com/s3.png", Description = "Vegetative growth" },
				new Spectrum { SpectrumId = 4, Type = ESpectrumType.DualSpectrum, ColorHexCode = "#4B0082", SpectrumChartUrl = "https://cdn.example.com/s4.png", Description = "Terpene production" },
				new Spectrum { SpectrumId = 5, Type = ESpectrumType.Customized, ColorHexCode = "#4B8875", SpectrumChartUrl = "https://cdn.example.com/s4.png", Description = "Terpene production" }

			);

			// 4. Seed PowerSupply
			modelBuilder.Entity<PowerSupply>().HasData(
				new PowerSupply { PowerSupplyId = 1, PowerSupplyType = EPowerSypplyType.Internal, Voltage = 48 },
				new PowerSupply { PowerSupplyId = 2, PowerSupplyType = EPowerSypplyType.Driverless, Voltage = 24 },
				new PowerSupply { PowerSupplyId = 3, PowerSupplyType = EPowerSypplyType.External, Voltage = 36 },
				new PowerSupply { PowerSupplyId = 4, PowerSupplyType = EPowerSypplyType.Removable, Voltage = 54 }

			);
			modelBuilder.Entity<NutrientType>().HasData(
			new NutrientType
			{
				NutrientTypeId = 1,
				NutrientName = "Base Nutrients",
				Description = "Essential N-P-K foundation for all plant stages."
			},
			new NutrientType
			{
				NutrientTypeId = 2,
				NutrientName = "Root Stimulators",
				Description = "Enhances root development and nutrient uptake efficiency."
			},
			new NutrientType
			{
				NutrientTypeId = 3,
				NutrientName = "Bloom Boosters",
				Description = "High Phosphorus and Potassium for massive flower production."
			},
			new NutrientType
			{
				NutrientTypeId = 4,
				NutrientName = "Cal-Mag Supplements",
				Description = "Prevents common deficiencies in Coco Coir or RO water."
			},
			new NutrientType
			{
				NutrientTypeId = 5,
				NutrientName = "pH Adjusters",
				Description = "Solutions to maintain optimal pH levels (5.5 - 6.5)."
			});
		}
	}
}
