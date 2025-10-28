// tạo để chạy dal kết nối db k cần qua config api
namespace DAL.Dbcontext
{
	public class CannabisAccessorriesDbContextFactory :  IDesignTimeDbContextFactory<CannabisAccessorriesDBContext>
	{
		public CannabisAccessorriesDBContext CreateDbContext(string[] args)
		{
			var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

			// load config từ appsettings.Development.json
			var config = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: false)
				.AddJsonFile($"appsettings.{environment}.json", optional: true)
				.Build();

			var connectionString = config.GetConnectionString("CannabisAccessorriesDB");

			var optionsBuilder = new DbContextOptionsBuilder<CannabisAccessorriesDBContext>();
			optionsBuilder.UseSqlServer(connectionString);

			return new CannabisAccessorriesDBContext(optionsBuilder.Options);
		}
	}
}
