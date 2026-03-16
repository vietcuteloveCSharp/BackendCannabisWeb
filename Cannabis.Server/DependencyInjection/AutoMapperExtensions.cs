namespace Cannabis.Server.DependencyInjection
{
	public static class AutoMapperExtensions
	{
		public static IServiceCollection AddApplicationAutoMapper(this IServiceCollection services)
		{
			services.AddAutoMapper(cfg =>
			{
				cfg.AddProfile<AddressMappingProfile>();
				cfg.AddProfile<BrandMappingProfile>();
				cfg.AddProfile<BreederMappingProfile>();
				cfg.AddProfile<CarbonFilerMappingProfile>();
				cfg.AddProfile<ChipModelMappingProfile>();
				cfg.AddProfile<ClassificationMappingProfile>();
				cfg.AddProfile<CoolingSystemMappingProfile>();
				cfg.AddProfile<DehumidifierMappingProfile>();
				cfg.AddProfile<GrowTentMappingProfile>();
				cfg.AddProfile<GrowLightMappingProfile>();
				cfg.AddProfile<NutrientMappingProfile>();
				cfg.AddProfile<NutrientTypeMappingProfile>();
				cfg.AddProfile<PowerSupplyMappingProfile>();
				cfg.AddProfile<RefreshTokenMappingProfile>();
				cfg.AddProfile<RoleMappingProfile>();
				cfg.AddProfile<SpectrumMappingProfile>();
				cfg.AddProfile<UserMappingProfile>();
				cfg.AddProfile<CategoryMappingProfile>();
				cfg.AddProfile<ProductMappingProfile>();
			});
			return services;
		}
	}
}
