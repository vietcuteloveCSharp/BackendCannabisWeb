namespace Cannabis.Server.DependencyInjection
{
	public static class InfrastructureExtensions
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
		{
			services.Configure<MailSettings>(config.GetSection("Email"));
			services.Configure<RedisSetings>(config.GetSection("Redis"));
			services.AddScoped<IEmailService, EmailService>();
			services.AddSingleton<IRedisService, RedisService>();
			services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
			return services;
		}
	}
}
