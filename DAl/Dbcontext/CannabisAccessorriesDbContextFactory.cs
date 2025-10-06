// tạo để chạy dal kết nối db k cần qua config api
namespace DAL.Dbcontext
{
	public class CannabisAccessorriesDbContextFactory :  IDesignTimeDbContextFactory<CannabisAccessorriesDBContext>
	{
		public CannabisAccessorriesDBContext CreateDbContext(string[] args)
		{
			// load config từ appsettings.Development.json
			var config = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.Migrations.json", optional: false)
				.Build();

			var connectionString = config.GetConnectionString("CannabisAccessorriesDB");

			var optionsBuilder = new DbContextOptionsBuilder<CannabisAccessorriesDBContext>();
			optionsBuilder.UseSqlServer(connectionString);

			return new CannabisAccessorriesDBContext(optionsBuilder.Options);
		}
	}
}
