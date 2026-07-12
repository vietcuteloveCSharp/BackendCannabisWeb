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
					// ==========================
					// Seed Role
					// ==========================
					Role adminRole;
					Role customerRole;
					if (!await context.Set<Role>().AnyAsync())
					{
						adminRole = new Role
						{
							RoleName = "Admin",
							Description = "System Administrator",
							CreatedAt = DateTime.UtcNow,
							CreatedBy = 1,
							UpdatedBy = 1
						};
						customerRole = new Role
						{
							RoleName = "Customer",
							Description = "Customer",
							CreatedAt = DateTime.UtcNow,
							CreatedBy = 1,
							UpdatedBy = 1
						};
						await context.Set<Role>().AddRangeAsync(adminRole, customerRole);
						await context.SaveChangesAsync();
					}
					else
					{
						adminRole = await context.Set<Role>()
							.FirstAsync(x => x.RoleName == "Admin");

						customerRole = await context.Set<Role>()
							.FirstAsync(x => x.RoleName == "Customer");
					}
					// ==========================
					// Seed User Status
					// ==========================
					StaffStatus activeStatus;
					StaffStatus inactiveStatus;

					if (!await context.Set<StaffStatus>().AnyAsync())
					{
						activeStatus = new StaffStatus
						{
							Code = "ACTIVE",
							Name = "Active"
						};
						inactiveStatus = new StaffStatus
						{
							Code = "INACTIVE",
							Name = "Inactive"
						};

						await context.Set<StaffStatus>().AddAsync(activeStatus);
						await context.SaveChangesAsync();
					}
					else
					{
						activeStatus = await context.Set<StaffStatus>()
							.FirstAsync(x => x.Code == "ACTIVE");
						activeStatus = await context.Set<StaffStatus>()
							.FirstAsync(x => x.Code == "INACTIVE");
					}

					// ==========================
					// Seed Admin
					// ==========================
					if (!await context.Set<Staff>()
						.AnyAsync(x => x.Username == "admin"))
					{
						var admin = new Staff
						{
							Username = "admin",
							PasswordHash = "AQAAAAIAAYagAAAAEImX588zM4W0XlS+3Dsh6Bv6L3vU/3HInG5O3oN1uU6VvO+V/vV6=", // 123456
							Name = "System Admin",
							Email = "admin@cannabis.com",
							PhoneNumber = "0123456789",

							RoleId = adminRole.Id,
							StatusId = activeStatus.Id,

							CreatedAt = DateTime.UtcNow,
							CreatedBy = 1,
							UpdatedBy = 1
						};

						await context.Set<Staff>().AddAsync(admin);
						await context.SaveChangesAsync();
					}

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