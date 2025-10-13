namespace TestsCannabis.DataSeed
{
	public static class DbSeeder
	{
		public static async Task SeedAll(CannabisAccessorriesDBContext db)
		{
			SeedRoles(db);
			SeedUsers(db);
			SeedAddresses(db);
			SeedBrands(db);
			SeedCarbonFilters(db);
			SeedClassifications(db);
			SeedSpectrums(db);
			SeedChipModels(db);
			SeedProductImages(db);
			SeedNutrientTypes(db);
			SeedNutrients(db);
			await db.SaveChangesAsync();
		}
		private static void SeedRoles(CannabisAccessorriesDBContext db)
		{
			if (db.Roles.Any()) return;
			db.Roles.AddRange(new List<Role>
			{
				new Role
				{
					RoleId = 1,
					RoleName = ERoleName.Admin,
					Description = "Admin",
					CreatedAt = DateTime.Now,
					UpdatedAt = DateTime.Now
				},
				new Role
				{
					RoleId =2,
					RoleName =ERoleName.Employee,
					Description  ="Employee",
					CreatedAt= DateTime.Now,
					UpdatedAt= DateTime.Now
				},
				new Role
				{
					RoleId =3,
					RoleName =ERoleName.User,
					Description  ="User",
					CreatedAt= DateTime.Now,
					UpdatedAt= DateTime.Now
				}
			});
		}
		private static void SeedUsers(CannabisAccessorriesDBContext db)
		{
			if (db.Users.Any()) return;
			var password = "Vuvietanh1!";
			var hasher = new PasswordHasher<User>();
			var users = new List<User>
			{
				new User
				{
					Username = "testadmin01",
					Name = "Nguyễn Văn A",
					Email = "admin01@example.com",
					PhoneNumber = "0912345671",
					CreatedAt = DateTime.Now,
					UpdatedAt = DateTime.Now,
					Status = EUserStatus.Active,
					RoleId = 1 // Admin
				},
				new User
				{
					Username = "testemployee01",
					Name = "Trần Thị B",
					Email = "employee01@example.com",
					PhoneNumber = "0912345672",
					RoleId = 2, // Employee
					Status = EUserStatus.Active,
					CreatedAt = DateTime.Now,
					UpdatedAt = DateTime.Now
				},
				new User
				{
					Username = "testuser01",
					Name = "Lê Văn C",
					Email = "user01@example.com",
					PhoneNumber = "0912345673",
					RoleId = 3, // User
					Status = EUserStatus.Active,
					CreatedAt = DateTime.Now,
					UpdatedAt = DateTime.Now
				},
				new User
				{
					Username = "testadmin02",
					Name = "Phạm Thị D",
					Email = "admin02@example.com",
					PhoneNumber = "0912345674",
					RoleId = 1,
					Status = EUserStatus.Active,
					CreatedAt = DateTime.Now,
					UpdatedAt = DateTime.Now
				},
				 new User
				 {
					Username = "guest01",
					Name = "Hoàng Văn E",
					Email = "guest01@example.com",
					PhoneNumber = "0912345675",
					RoleId = 2,
					Status = EUserStatus.Active,
					CreatedAt = DateTime.Now,
					UpdatedAt = DateTime.Now
				 },
			};
			// Hash password cho tất cả user
			foreach (var user in users)
			{
				user.HashPassword = hasher.HashPassword(user, password);
			}
			db.Users.AddRange(users);

		}
		private static void SeedAddresses(CannabisAccessorriesDBContext db)
		{
			if (db.Addresses.Any()) return;

			var addresses = new List<Address>
			{
				new Address
				{
					UserId = 1,
					Country = "Vietnam",
					Province = "Hanoi",
					District = "Ba Dinh",
					Commune = "Dien Bien",
					Road_Village_Hamlet = "Nguyen Chi Thanh",
					HouseNumber = "12A",
					PostalCode = "100000",
					IsDefault = true,
					CreatedAt = DateTime.Now
				},
				new Address
				{
					UserId = 2,
					Country = "Vietnam",
					Province = "Ho Chi Minh",
					District = "District 1",
					Commune = "Ben Nghe",
					Road_Village_Hamlet = "Le Loi",
					HouseNumber = "45B",
					PostalCode = "700000",
					IsDefault = true,
					CreatedAt = DateTime.Now
				},
				new Address
				{
					UserId = 3,
					Country = "Vietnam",
					Province = "Da Nang",
					District = "Hai Chau",
					Commune = "Thach Thang",
					Road_Village_Hamlet = "Pham Van Dong",
					HouseNumber = "101",
					PostalCode = "550000",
					IsDefault = true,
					CreatedAt = DateTime.Now
				},
				new Address
				{
					UserId = 4,
					Country = "Vietnam",
					Province = "Hai Phong",
					District = "Le Chan",
					Commune = "Dong Khe",
					Road_Village_Hamlet = "Tran Phu",
					HouseNumber = "77",
					PostalCode = "180000",
					IsDefault = true,
					CreatedAt = DateTime.Now
				},
				new Address
				{
					UserId = 5,
					Country = "Vietnam",
					Province = "Can Tho",
					District = "Ninh Kieu",
					Commune = "Tan An",
					Road_Village_Hamlet = "Nguyen Van Cu",
					HouseNumber = "22C",
					PostalCode = "900000",
					IsDefault = true,
					CreatedAt = DateTime.Now
				}
			};
			db.Addresses.AddRange(addresses);
		}
		private static void SeedBrands(CannabisAccessorriesDBContext db)
		{
			if (db.Brands.Any()) return;

			var brands = new List<Brand>
			{
				new Brand
				{
					BrandName = "GreenLeaf",
					Country = "USA",
					Description = "Premium cannabis accessories brand.",
					Website = "https://www.greenleaf.com",
					IsActive = true,
					CreatedAt = DateTime.Now
				},
				new Brand
				{
					BrandName = "HerbalEssence",
					Country = "Canada",
					Description = "Natural and organic cannabis products.",
					Website = "https://www.herbalessence.ca",
					IsActive = true,
					CreatedAt = DateTime.Now
				},
				new Brand
				{
					BrandName = "CannaTech",
					Country = "Netherlands",
					Description = "Innovative cannabis technology solutions.",
					Website = "https://www.cannatech.nl",
					IsActive = true,
					CreatedAt = DateTime.Now
				},
				new Brand
				{
					BrandName = "BudMasters",
					Country = "USA",
					Description = "High-quality cannabis cultivation equipment.",
					Website = "https://www.budmasters.com",
					IsActive = true,
					CreatedAt = DateTime.Now
				},
				new Brand
				{
					BrandName = "LeafyLux",
					Country = "UK",
					Description = "Luxury cannabis lifestyle products.",
					Website = "https://www.leafylux.co.uk",
					IsActive = true,
					CreatedAt = DateTime.Now
				},new Brand
				{
					BrandName = "Mars Hydro",
					Country = "China",
					Description = "Popular LED grow light manufacturer",
					Website = "https://marshydro.com",
					IsActive = true,
					CreatedAt = DateTime.UtcNow
				}
			};
			db.Brands.AddRange(brands);
		}
		private static void SeedCarbonFilters(CannabisAccessorriesDBContext db)
		{
			if (db.CarbonFilters.Any()) return;

			var greenLeaf = db.Brands.FirstOrDefault(b => b.BrandName == "GreenLeaf");
			var herbalEssence = db.Brands.FirstOrDefault(b => b.BrandName == "HerbalEssence");
			var budMasters = db.Brands.FirstOrDefault(b => b.BrandName == "BudMasters");
			var leafyLux = db.Brands.FirstOrDefault(b => b.BrandName == "LeafyLux");
			var marsHydro = db.Brands.FirstOrDefault(b => b.BrandName == "Mars Hydro");

			var carbonFilters = new List<CarbonFilter>
			{
			 	new CarbonFilter
				{
					AirflowRate = "500 m³/h",
					BrandId = greenLeaf?.BrandId ?? 1,
					Quantity = 20,
					Price = 120.50m,
					FilterMaterial = "Activated Carbon",
					Diameter = 15.5m,
					Length = 40.0m,
					Lifespan = 12000, // giờ
                    MinTemperature = 5.0m,
					MaxTemperature = 55.0m,
					Description = "High efficiency carbon filter for medium grow tents",
					WarrantyPeriod = 12,
					ModelNumber = "MH-CF500",
					CreatedAt = DateTime.UtcNow
				},
				new CarbonFilter
				{
					AirflowRate = "800 m³/h",
					BrandId = herbalEssence?.BrandId ?? 2,
					Quantity = 10,
					Price = 180.75m,
					FilterMaterial = "Virgin Carbon",
					Diameter = 20.0m,
					Length = 60.0m,
					Lifespan = 15000,
					MinTemperature = 0.0m,
					MaxTemperature = 60.0m,
					Description = "Premium carbon filter with long lifespan",
					WarrantyPeriod = 24,
					ModelNumber = "GG-CF800",
					CreatedAt = DateTime.UtcNow
				},
				new CarbonFilter
				{
					AirflowRate = "1000 CFM",
					BrandId = budMasters?.BrandId ?? 3,
					Quantity = 8,
					Price = 250.00m,
					FilterMaterial = "Activated Carbon",
					Diameter = 10.0m,
					Length = 16.0m,
					Lifespan = 1800,
					MinTemperature = 5.0m,
					MaxTemperature = 40.0m,
					Description = "Premium carbon filter for optimal air purification.",
					WarrantyPeriod = 24,
					ModelNumber = "CF-1000C",
					CreatedAt = DateTime.Now
				}
			};
			db.CarbonFilters.AddRange(carbonFilters);
		}
		private static void SeedClassifications(CannabisAccessorriesDBContext db)
		{
			if (db.Classifications.Any()) return; // tránh seed trùng khi test lại

			var classifications = new List<Classification>
		{
			new Classification
			{
				ClassificationId = 1,
				ClassificationName = "Indica",
				Quantity = 120,
				Description = "Dòng cannabis thân thấp, hiệu ứng thư giãn.",
				IsActive = true,
				CreatedAt = DateTime.UtcNow
			},
			new Classification
			{
				ClassificationId = 2,
				ClassificationName = "Sativa",
				Quantity = 90,
				Description = "Dòng cao, hiệu ứng kích thích và tập trung.",
				IsActive = true,
				CreatedAt = DateTime.UtcNow
			},
			new Classification
			{
				ClassificationId = 3,
				ClassificationName = "Hybrid",
				Quantity = 200,
				Description = "Lai giữa Indica và Sativa, cân bằng hiệu ứng.",
				IsActive = true,
				CreatedAt = DateTime.UtcNow
			},
			new Classification
			{
				ClassificationId = 4,
				ClassificationName = "CBD Dominant",
				Quantity = 60,
				Description = "Hàm lượng CBD cao, dùng cho y tế.",
				IsActive = false,
				CreatedAt = DateTime.UtcNow
			}
		};

			db.Classifications.AddRange(classifications);
		}
		private static void SeedSpectrums(CannabisAccessorriesDBContext db)
		{
			if (db.Spectrums.Any()) return;

			var spectrums = new List<Spectrum>
	{
		new Spectrum
		{
			SpectrumId = 1,
			Type = ESpectrumType.FullSpectrum,
			Description = "Phổ ánh sáng toàn dải, dùng cho cả giai đoạn Veg và Bloom.",
			CreatedAt = DateTime.UtcNow
		},
		new Spectrum
		{
			SpectrumId = 2,
			Type = ESpectrumType.Veg,
			Description = "Phổ ánh sáng xanh, thích hợp cho giai đoạn sinh trưởng (vegetative).",
			CreatedAt = DateTime.UtcNow
		},
		new Spectrum
		{
			SpectrumId = 3,
			Type = ESpectrumType.Bloom,
			Description = "Phổ ánh sáng đỏ, tăng năng suất giai đoạn ra hoa (bloom).",
			CreatedAt = DateTime.UtcNow
		},
		new Spectrum
		{
			SpectrumId = 4,
			Type = ESpectrumType.UV,
			Description = "Ánh sáng tia cực tím giúp tăng sản xuất trichome.",
			CreatedAt = DateTime.UtcNow
		},
		new Spectrum
		{
			SpectrumId = 5,
			Type = ESpectrumType.IR,
			Description = "Ánh sáng hồng ngoại giúp cây kéo dài thân và kích thích ra hoa nhanh hơn.",
			CreatedAt = DateTime.UtcNow
		}
	};

			db.Spectrums.AddRange(spectrums);
		}
		private static void SeedChipModels(CannabisAccessorriesDBContext db)
		{
			if (db.ChipModels.Any()) return;

			var chipModels = new List<ChipModel>
	{
		new ChipModel
		{
			ChipModelId = 1,
			Manufacturer = "Samsung",
			ModelChip = "LM301H",
			Generation = "Gen 2",
			Efficiency = 3.10m,
			Description = "Chip LED hiệu suất cao, phổ biến trong các dòng đèn full-spectrum cao cấp.",
			CreatedAt = DateTime.UtcNow
		},
		new ChipModel
		{
			ChipModelId = 2,
			Manufacturer = "Samsung",
			ModelChip = "LM301B",
			Generation = "Gen 1",
			Efficiency = 2.92m,
			Description = "Chip LED thế hệ cũ, hiệu suất khá tốt, giá thành hợp lý.",
			CreatedAt = DateTime.UtcNow
		},
		new ChipModel
		{
			ChipModelId = 3,
			Manufacturer = "Osram",
			ModelChip = "Oslon SSL 80",
			Generation = "Gen 3",
			Efficiency = 2.85m,
			Description = "Chip đỏ chuyên dụng cho giai đoạn ra hoa, phổ đỏ 660nm.",
			CreatedAt = DateTime.UtcNow
		},
		new ChipModel
		{
			ChipModelId = 4,
			Manufacturer = "Cree",
			ModelChip = "XP-G3",
			Generation = "Gen 2",
			Efficiency = 2.75m,
			Description = "Chip chất lượng cao của Cree, hiệu suất tốt, tỏa nhiệt thấp.",
			CreatedAt = DateTime.UtcNow
		},
		new ChipModel
		{
			ChipModelId = 5,
			Manufacturer = "Bridgelux",
			ModelChip = "EB Gen 3",
			Generation = "Gen 3",
			Efficiency = 2.65m,
			Description = "Giải pháp LED phổ thông, giá thấp, phù hợp đèn nhỏ.",
			CreatedAt = DateTime.UtcNow
		}
	};

			db.ChipModels.AddRange(chipModels);
		}
		private static void SeedProductImages(CannabisAccessorriesDBContext db)
		{
			if (db.ProductImages.Any()) return;

			var productImages = new List<ProductImage>
			{
				// Product 1
				new ProductImage
				{
					ProductImageId = 1,
					ProductId = 1,
					ImageUrl = "https://res.cloudinary.com/demo/image/upload/v1/products/growlight_1_main.jpg",
					IsMainImage = true,
					CreatedAt = DateTime.UtcNow
				},
				new ProductImage
				{
					ProductImageId = 2,
					ProductId = 1,
					ImageUrl = "https://res.cloudinary.com/demo/image/upload/v1/products/growlight_1_side.jpg",
					IsMainImage = false,
					CreatedAt = DateTime.UtcNow
				},

				// Product 2
				new ProductImage
				{
					ProductImageId = 3,
					ProductId = 2,
					ImageUrl = "https://res.cloudinary.com/demo/image/upload/v1/products/filter_2_main.jpg",
					IsMainImage = true,
					CreatedAt = DateTime.UtcNow
				},
				new ProductImage
				{
				ProductImageId = 4,
				ProductId = 2,
				ImageUrl = "https://res.cloudinary.com/demo/image/upload/v1/products/filter_2_detail.jpg",
				IsMainImage = false,
				CreatedAt = DateTime.UtcNow
				},

				// Product 3
				new ProductImage
				{
					ProductImageId = 5,
					ProductId = 3,
					ImageUrl = "https://res.cloudinary.com/demo/image/upload/v1/products/tent_3_main.jpg",
					IsMainImage = true,
					CreatedAt = DateTime.UtcNow
				},
				new ProductImage
				{
					ProductImageId = 6,
					ProductId = 3,
					ImageUrl = "https://res.cloudinary.com/demo/image/upload/v1/products/tent_3_inside.jpg",
					IsMainImage = false,
					CreatedAt = DateTime.UtcNow
				}
			};

			db.ProductImages.AddRange(productImages);
		}
		private static void SeedNutrients(CannabisAccessorriesDBContext db)
		{
			if (db.Nutrients.Any()) return;

			var nutrients = new List<Nutrient>
	{
		new Nutrient
		{
			NutrientId = 1,
			ProductId = 1,
			BrandId = 1,
			NutrientTypeId = 1, // Grow
            Quantity = 50,
			Price = 350000m,
			VolumeMl = 1000,
			Ingredients = "Nitrogen, Phosphorus, Potassium, Magnesium, Calcium",
			NpkRatio = "3-1-2",
			IsOrganic = true,
			Description = "Dung dịch dinh dưỡng hữu cơ cho giai đoạn sinh trưởng (grow).",
			ExpirationDate = DateTime.UtcNow.AddYears(1),
			StorageInstructions = "Bảo quản nơi khô ráo, tránh ánh sáng trực tiếp.",
			CreatedAt = DateTime.UtcNow
		},
		new Nutrient
		{
			NutrientId = 2,
			ProductId = 2,
			BrandId = 1,
			NutrientTypeId = 2, // Bloom
            Quantity = 40,
			Price = 420000m,
			VolumeMl = 1000,
			Ingredients = "Phosphorus, Potassium, Sulfur, Iron",
			NpkRatio = "1-3-2",
			IsOrganic = false,
			Description = "Dung dịch dinh dưỡng khoáng cho giai đoạn ra hoa (bloom).",
			ExpirationDate = DateTime.UtcNow.AddYears(1),
			StorageInstructions = "Lắc đều trước khi dùng, tránh nhiệt độ cao.",
			CreatedAt = DateTime.UtcNow
		},
		new Nutrient
		{
			NutrientId = 3,
			ProductId = 3,
			BrandId = 2,
			NutrientTypeId = 3, // Micro
            Quantity = 30,
			Price = 280000m,
			VolumeMl = 500,
			Ingredients = "Iron, Zinc, Manganese, Copper, Boron",
			NpkRatio = "0-0-1",
			IsOrganic = false,
			Description = "Dung dịch vi lượng hỗ trợ tăng hấp thu dinh dưỡng.",
			ExpirationDate = DateTime.UtcNow.AddYears(2),
			StorageInstructions = "Đóng kín nắp sau khi mở, bảo quản dưới 30°C.",
			CreatedAt = DateTime.UtcNow
		}
	};

			db.Nutrients.AddRange(nutrients);
		}
		private static void SeedNutrientTypes(CannabisAccessorriesDBContext db)
		{
			if (db.NutrientTypes.Any()) return;

			var nutrientTypes = new List<NutrientType>
	{
		new NutrientType
		{
			NutrientTypeId = 1,
			NutrientName = "Grow",
			Description = "Dinh dưỡng cho giai đoạn sinh trưởng (vegetative).",
			CreatedAt = DateTime.UtcNow
		},
		new NutrientType
		{
			NutrientTypeId = 2,
			NutrientName = "Bloom",
			Description = "Dinh dưỡng cho giai đoạn ra hoa (flowering).",
			CreatedAt = DateTime.UtcNow
		},
		new NutrientType
		{
			NutrientTypeId = 3,
			NutrientName = "Micro",
			Description = "Dung dịch vi lượng hỗ trợ tăng cường hấp thụ.",
			CreatedAt = DateTime.UtcNow
		},
		new NutrientType
		{
			NutrientTypeId = 4,
			NutrientName = "Cal-Mag",
			Description = "Bổ sung Canxi và Magie cho cây.",
			CreatedAt = DateTime.UtcNow
		},
		new NutrientType
		{
			NutrientTypeId = 5,
			NutrientName = "Additives",
			Description = "Các phụ gia hỗ trợ như enzyme, carbohydrate, booster,...",
			CreatedAt = DateTime.UtcNow
		}
	};

			db.NutrientTypes.AddRange(nutrientTypes);
		}



	}
}


