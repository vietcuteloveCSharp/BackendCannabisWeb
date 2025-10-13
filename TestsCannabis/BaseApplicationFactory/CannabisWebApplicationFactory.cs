using Cannabis.Server.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestsCannabis.Mocks;

namespace TestsCannabis.BaseApplicationFactory
{
	public class CannabisWebApplicationFactory : WebApplicationFactory<Program>
	{
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
			return base.CreateHost(builder);
		}
		protected override void ConfigureWebHost(IWebHostBuilder builder)
		{

			builder.ConfigureServices(services =>
			{
				// Xoá DbContext cũ
				var descriptor = services.SingleOrDefault(
					d => d.ServiceType == typeof(DbContextOptions<CannabisAccessorriesDBContext>));
				if (descriptor != null)
					services.Remove(descriptor);

				// Thêm DbContext với InMemoryDb
				services.AddDbContext<CannabisAccessorriesDBContext>(options =>
				{
					options.UseInMemoryDatabase("TestAPI");
					//options.UseInternalServiceProvider(provider);
				});
				
				services.AddSingleton<IRedisService,FakeRedisService>();
				services.AddSingleton<IEmailService, FakeEmailService>();

				// ✅ khởi tạo db và seed data 
				var sp = services.BuildServiceProvider();
				using var scope = sp.CreateScope();
				var db = scope.ServiceProvider.GetRequiredService<CannabisAccessorriesDBContext>();
				db.Database.EnsureCreated();
				// seed data
				if (!db.Users.Any())
				{
					DbSeeder.SeedAll(db).GetAwaiter().GetResult();
				}
			});
		}

	}

}
