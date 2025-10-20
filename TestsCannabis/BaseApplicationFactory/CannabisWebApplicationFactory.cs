using Cannabis.Server.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
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
			base.ConfigureWebHost(builder);

			builder.ConfigureServices(services =>
			{
				// Xoá DbContext cũ
				var descriptor = services.SingleOrDefault(
					d => d.ServiceType == typeof(DbContextOptions<CannabisAccessorriesDBContext>));
				var redisDescriptor = services.SingleOrDefault(
				d => d.ServiceType == typeof(IRedisService));
				services.AddAuthentication("TestScheme").AddScheme<AuthenticationSchemeOptions, TestAuthHandler_NoPass>(
							"TestScheme", options => { });
				if (descriptor != null)
					services.Remove(descriptor);
				if (redisDescriptor != null)
				{
					services.Remove(redisDescriptor);
				}

				// Thêm DbContext với InMemoryDb
				services.AddDbContext<CannabisAccessorriesDBContext>(options =>
				{
					options.UseInMemoryDatabase("TestAPI");
					//options.UseInternalServiceProvider(provider);
				});

				services.AddSingleton<FakeRedisService>();
				services.AddSingleton<IRedisService>(sp =>
					sp.GetRequiredService<FakeRedisService>());
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
