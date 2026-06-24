using Cannabis.Server.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Hosting;
using Service.Interfaces.Auth;
using TestsCannabis.Mocks;

namespace TestsCannabis.BaseApplicationFactory
{
	public class CannabisWebApplicationFactory : WebApplicationFactory<Program>
	{
		
		protected override void ConfigureWebHost(IWebHostBuilder builder)
		{
			base.ConfigureWebHost(builder);

			builder.ConfigureServices((context,services) =>
			{
				// 1. Xóa cấu hình DB thật (SQL Server) để thay bằng InMemory
				var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<CannabisAccessoriesDBContext>));

				if (descriptor != null) services.Remove(descriptor);

				services.AddDbContext<CannabisAccessoriesDBContext>(options =>
				{
					// Dùng tên DB ngẫu nhiên để các Test Case không đè dữ liệu lên nhau
					options.UseInMemoryDatabase("IntegrationTest_Shared_DB");
				});
				services.Configure<JwtConfig>(context.Configuration.GetSection("Jwt"));
				// 2. Mock các dịch vụ bên ngoài (Email, Redis)
				services.AddSingleton<FakeRedisService>();
				services.AddSingleton<IRedisService>(sp =>
					sp.GetRequiredService<FakeRedisService>());
				services.AddSingleton<IEmailService, FakeEmailService>();

				//services.AddAuthentication("TestScheme")
				//	.AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("TestScheme", options => { });				
			});
			
		}
		protected override IHost CreateHost(IHostBuilder builder)
		{
			

			builder.UseEnvironment("Development");

			builder.ConfigureAppConfiguration((context, config) =>
			{

				var apiPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../../Cannabis.Server"));
				config.SetBasePath(apiPath);
				config.AddJsonFile("appsettings.json", false, true)
					  .AddJsonFile("appsettings.Development.json", true, true)
					  .AddEnvironmentVariables();
			});
			// 2. Build Host từ builder
			var host = base.CreateHost(builder);

			// 3. Thực hiện Seed Data NGAY TẠI ĐÂY (Sử dụng đúng Service Provider của Host)
			using (var scope = host.Services.CreateScope())
			{
				var db = scope.ServiceProvider.GetRequiredService<CannabisAccessoriesDBContext>();

				// Đảm bảo Database InMemory được làm sạch và tạo mới
				db.Database.EnsureDeleted();
				db.Database.EnsureCreated();
				db.ChangeTracker.Clear();
				// Nạp toàn bộ dữ liệu mẫu (Roles, Users...)
				// Dùng .GetAwaiter().GetResult() vì CreateHost không cho phép async trực tiếp
				DbSeeder.SeedAll(db).GetAwaiter().GetResult();
			}

			return host;
		}
	}

}
