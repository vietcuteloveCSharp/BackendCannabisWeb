namespace DAl.Data
{
	public class DbInitializer
	{
		public static void Seed(ModelBuilder modelBuilder)
		{
			// 1. Seed ChipModel
			modelBuilder.Entity<ChipModel>().HasData(
				new ChipModel { Id = 1, Manufacturer = "Samsung", ModelChip = "LM301H", ModelName = "Evo", Efficiency = 3.10m, Description = "Top tier for horticulture" },
				new ChipModel { Id = 2, Manufacturer = "Osram", ModelChip = "GH CSSRM4.24", ModelName = "Oslon Square", Efficiency = 4.00m, Description = "Hyper Red 660nm" },
				new ChipModel { Id = 3, Manufacturer = "Cree", ModelChip = "JK2835", ModelName = "J Series", Efficiency = 2.80m, Description = "Cost-effective solution" },
				new ChipModel { Id = 4, Manufacturer = "Bridgelux", ModelChip = "BXEB-L0340", ModelName = "Vero 29", Efficiency = 2.60m, Description = "High power COB" },
				new ChipModel { Id = 5, Manufacturer = "Seoul", ModelChip = "MJT-3030", ModelName = "SunLike", Efficiency = 2.75m, Description = "Full spectrum natural light" }
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
				new Spectrum
				{
					Id = 1,
					Type = ESpectrumType.FullSpectrum,
					ColorHexCode = "#FDF4E3", // Trắng nắng (Sun-like)
					ColorTemperatureK = 3500,
					CRI = 90,
					SpectrumChartUrl = "/images/spectrums/full-spectrum.jpg"
				},
				new Spectrum
				{
					Id = 2,
					Type = ESpectrumType.Vegetative,
					ColorHexCode = "#1E90FF", // Dodger Blue
					ColorTemperatureK = 6500,
					CRI = 85,
					SpectrumChartUrl = "/images/spectrums/veg-spectrum.jpg"
				},
				new Spectrum
				{
					Id = 3,
					Type = ESpectrumType.Flowering,
					ColorHexCode = "#FF4500", // Orange Red (Kích thích hoa)
					ColorTemperatureK = 2700,
					CRI = 88,
					SpectrumChartUrl = "/images/spectrums/bloom-spectrum.jpg"
				}
			);

			// 4. Seed PowerSupply
			modelBuilder.Entity<PowerSupply>().HasData(
				new PowerSupply { Id = 1, PowerSupplyType = EPowerSypplyType.Internal, Voltage = 48 },
				new PowerSupply { Id = 2, PowerSupplyType = EPowerSypplyType.Driverless, Voltage = 24 },
				new PowerSupply { Id = 3, PowerSupplyType = EPowerSypplyType.External, Voltage = 36 },
				new PowerSupply { Id = 4, PowerSupplyType = EPowerSypplyType.Removable, Voltage = 54 }

			);
			modelBuilder.Entity<NutrientType>().HasData(
			new NutrientType
			{
				Id = 1,
				NutrientName = "Base Nutrients",
				Description = "Essential N-P-K foundation for all plant stages."
			},
			new NutrientType
			{
				Id = 2,
				NutrientName = "Root Stimulators",
				Description = "Enhances root development and nutrient uptake efficiency."
			},
			new NutrientType
			{
				Id = 3,
				NutrientName = "Bloom Boosters",
				Description = "High Phosphorus and Potassium for massive flower production."
			},
			new NutrientType
			{
				Id = 4,
				NutrientName = "Cal-Mag Supplements",
				Description = "Prevents common deficiencies in Coco Coir or RO water."
			},
			new NutrientType
			{
				Id = 5,
				NutrientName = "pH Adjusters",
				Description = "Solutions to maintain optimal pH levels (5.5 - 6.5)."
			});
		}
	}
}
