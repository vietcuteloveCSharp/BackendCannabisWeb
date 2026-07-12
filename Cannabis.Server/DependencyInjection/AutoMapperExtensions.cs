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
				cfg.AddProfile<CategoryMappingProfile>();
				cfg.AddProfile<ProductMappingProfile>();
			});
			return services;
		}
	}
}
