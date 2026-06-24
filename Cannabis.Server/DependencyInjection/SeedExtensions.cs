using DAL.Dbcontext.SeedData; // Tham chiếu tới DAL

namespace Cannabis.Server.DependencyInjection
{
	public static class SeedExtensions
	{
		public static async Task SeedDatabaseAsync(this IApplicationBuilder app)
		{
			// Tạo Scope để lấy Scoped Service (DbContext)
			using var scope = app.ApplicationServices.CreateScope();
			var services = scope.ServiceProvider;

			try
			{
				var context = services.GetRequiredService<CannabisAccessoriesDBContext>();
				// Gọi sang DAL để thực hiện seed
				await DbInitializer.SeedData(context);
				Console.WriteLine("--> Database Seeded Successfully.");
			}
			catch (Exception ex)
			{
				var logger = services.GetRequiredService<ILogger<object>>();
				logger.LogError(ex, "--> Error during seeding database.");
			}
		}
	}
}