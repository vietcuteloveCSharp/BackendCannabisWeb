namespace Cannabis.Server.DependencyInjection
{
	public static class RepositoryExtensions
	{
		public static IServiceCollection AddApplicationRepositories(this IServiceCollection services)
		{
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped<IAddressRepository, AddressRepository>();
			services.AddScoped<IBrandRepository, BrandRepository>();
			services.AddScoped<IBreederRepository, BreederRepository>();
			services.AddScoped<ICarbonFilterRepository, CarbonFilterRepository>();
			services.AddScoped<IChipModelRepository, ChipModelRepository>();
			services.AddScoped<IClassificationRepository, ClassificationRepository>();
			services.AddScoped<ICoolingSystemRepository, CoolingSystemRepository>();
			services.AddScoped<IDehumidifierRepository, DehumidifierRepository>();
			services.AddScoped<IGrowLightRepository, GrowLightRepository>();
			services.AddScoped<IGrowTentRepository, GrowTentRepository>();
			services.AddScoped<INutrientRepository, NutrientRepository>();
			services.AddScoped<IAuditLogRepository, AuditLogRepository>();
			services.AddScoped<IRoleRepository, RoleRepository>();
			services.AddScoped<ISpectrumRepository, SpectrumRepository>();
			services.AddScoped<IPowerSupplyRepository, PowerSupplyRepository>();
			services.AddScoped<INutrientTypeRepository, NutrientTypeRepository>();
			services.AddScoped<IProductRepository, ProductRepository>();
			services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
			services.AddScoped<ISeedRepository, SeedRepository>();
			services.AddScoped<IUserRepository, UserRepository>();
			return services;
		}
	}
}
