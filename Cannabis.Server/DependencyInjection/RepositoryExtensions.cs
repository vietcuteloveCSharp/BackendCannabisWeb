namespace Cannabis.Server.DependencyInjection
{
	public static class RepositoryExtensions
	{
		public static IServiceCollection AddApplicationRepositories(this IServiceCollection services)
		{
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped<IAddressRepository, AddressRepository>();
			services.AddScoped<IBrandRepository, BrandRepository>();
			services.AddScoped<ICategoryRepository, CategoryRepository>();
			services.AddScoped<IAuditLogRepository, AuditLogRepository>();
			services.AddScoped<IRoleRepository, RoleRepository>();
			services.AddScoped<IProductRepository, ProductRepository>();
			services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
			services.AddScoped<IUserRepository, UserRepository>();
			return services;
		}
	}
}
