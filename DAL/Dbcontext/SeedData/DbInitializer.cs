using DAL.Dbcontext;
using DAL.Entities.Ship;

namespace DAL.Dbcontext.SeedData
{
	public static class DbInitializer
	{
		public static async Task SeedData(CannabisAccessoriesDBContext context)
		{
			// 1. Tạo chiến lược thực thi (Execution Strategy) để bọc Transaction
			var strategy = context.Database.CreateExecutionStrategy();

			await strategy.ExecuteAsync(async () =>
			{
				// 2. Kiểm tra dữ liệu bên trong Strategy
				if (await context.Set<Role>().AnyAsync()) return;

				// 3. Mở Transaction thông qua Strategy
				using var transaction = await context.Database.BeginTransactionAsync();
				try
				{
					// --- BẮT ĐẦU SEED LOGIC (Giữ nguyên phần này) ---
					var adminRole = new Role { RoleName = "Admin", Description = "Toàn quyền", CreatedAt = DateTime.UtcNow, CreatedBy = 1, UpdatedBy = 1 };
					var customerRole = new Role { RoleName = "Customer", Description = "Khách hàng", CreatedAt = DateTime.UtcNow, CreatedBy = 1, UpdatedBy = 1 };
					await context.Set<Role>().AddRangeAsync(adminRole, customerRole);

					var activeStatus = new UserStatus { Code = "ACTIVE", Name = "Đang hoạt động" };
					await context.Set<UserStatus>().AddAsync(activeStatus);

					await context.SaveChangesAsync();
					// --- 2. NHÓM ORDER & SHIPPING (Master data) ---
					var pendingStatus = new OrderStatus { Name = "Pending", Description = "Chờ xử lý", CreatedAt = DateTime.UtcNow, CreatedBy = 1, UpdatedBy = 1 };
					await context.Set<OrderStatus>().AddAsync(pendingStatus);

					var codMethod = new PaymentMethod { Name = "COD", Description = "Thanh toán khi nhận hàng", CreatedAt = DateTime.UtcNow, CreatedBy = 1, UpdatedBy = 1 };
					await context.Set<PaymentMethod>().AddAsync(codMethod);

					var unpaidStatus = new PaymentStatus { Name = "Unpaid", CreatedAt = DateTime.UtcNow, CreatedBy = 1, UpdatedBy = 1 };
					await context.Set<PaymentStatus>().AddAsync(unpaidStatus);

					var standardShip = new ShippingMethod { Name = "Standard", Description = "Giao hàng tiêu chuẩn", CreatedAt = DateTime.UtcNow, CreatedBy = 1, UpdatedBy = 1 };
					await context.Set<ShippingMethod>().AddAsync(standardShip);

					// --- 3. NHÓM CATALOG (Bắt buộc cho Product) ---
					var bongCat = new Category { CategoryName = "Bongs", CreatedAt = DateTime.UtcNow, CreatedBy = 1, UpdatedBy = 1 };
					await context.Set<Category>().AddAsync(bongCat);

					var rawBrand = new Brand { BrandName = "Raw", Country = "Spain", CreatedAt = DateTime.UtcNow, CreatedBy = 1, UpdatedBy = 1 };
					await context.Set<Brand>().AddAsync(rawBrand);

					await context.SaveChangesAsync();
					// --- 4. TẠO ADMIN USER (Bản ghi hoàn chỉnh) ---
					var admin = new User
					{
						Username = "admin",
						PasswordHash = "AQAAAAIAAYagAAAAEImX588zM4W0XlS+3Dsh6Bv6L3vU/3HInG5O3oN1uU6VvO+V/vV6=", // 123456
						Name = "System Admin",
						Email = "admin@cannabis.com",
						PhoneNumber = "0123456789",
						StatusId = activeStatus.Id,
						RoleId = adminRole.Id,
						CreatedAt = DateTime.UtcNow,
						CreatedBy = 1,
						UpdatedBy = 1
					};
					await context.Set<User>().AddAsync(admin);

					await context.Set<Category>().AddAsync(new Category { CategoryName = "Bongs", CreatedAt = DateTime.UtcNow, CreatedBy = 1, UpdatedBy = 1 });
					await context.Set<Brand>().AddAsync(new Brand { BrandName = "Raw", Country = "Spain", CreatedAt = DateTime.UtcNow, CreatedBy = 1, UpdatedBy = 1 });

					// 4. Lưu và Commit
					await context.SaveChangesAsync();
					await transaction.CommitAsync();
				}
				catch (Exception)
				{
					await transaction.RollbackAsync();
					throw; // Quăng lỗi để Strategy thực hiện Retry nếu cần
				}
			});
		}
	}
}