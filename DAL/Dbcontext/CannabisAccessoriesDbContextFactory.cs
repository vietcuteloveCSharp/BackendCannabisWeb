// tạo để chạy dal kết nối db k cần qua config api
namespace DAL.Dbcontext
{
	public class CannabisAccessoriesDbContextFactory :  IDesignTimeDbContextFactory<CannabisAccessoriesDBContext>
	{
		public CannabisAccessoriesDBContext CreateDbContext(string[] args)
		{
			// 1. Ép cứng môi trường để test
			string environment = "Development";

			// 2. Lấy đường dẫn tuyệt đối (Sửa lại cho chắc chắn)
			string basePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "Cannabis.Server"));

			Console.WriteLine($"[FACTORY DEBUG] Đang tìm config tại: {basePath}");

			var config = new ConfigurationBuilder()
				.SetBasePath(basePath)
				.AddJsonFile("appsettings.json", optional: false)
				.AddJsonFile($"appsettings.{environment}.json", optional: false)
				.Build();

			var connectionString = config.GetConnectionString("CannabisAccessoriesDB");

			if (string.IsNullOrEmpty(connectionString))
			{
				throw new Exception($"[FACTORY ERROR] Không lấy được ConnectionString! Path: {basePath}");
			}
			var optionsBuilder = new DbContextOptionsBuilder<CannabisAccessoriesDBContext>();
			optionsBuilder.UseSqlServer(connectionString);



			return new CannabisAccessoriesDBContext(optionsBuilder.Options);
		}
	}
}
