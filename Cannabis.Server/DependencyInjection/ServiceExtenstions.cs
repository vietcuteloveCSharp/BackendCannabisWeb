namespace Cannabis.Server.DependencyInjection{
	public static class ServiceExtenstions
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			services.AddScoped<IAddressService, AddressService>();
			services.AddScoped<IAuthService, AuthService>();
			services.AddScoped<IBrandService, BrandService>();
			services.AddScoped<IBreederService, BreederService>();
			services.AddScoped<ICarbonFilterService, CarbonFilterService>();
			services.AddScoped<IChipModelService, ChipModelService>();
			services.AddScoped<IClassificationService, ClassificationService>();
			services.AddScoped<ICoolingSystemService, CoolingSystemService>();
			services.AddScoped<IGrowTentService, GrowTentService>();
			services.AddScoped<INutrientService, NutrientService>();
			services.AddScoped<INutrientTypeService, NutrientTypeService>();
			services.AddScoped<IPowerSupplyService, PowerSupplyService>();
			services.AddScoped<IRefreshTokenService, RefreshTokenService>();
			services.AddScoped<IRoleService, RoleService>();
			services.AddScoped<ISpectrumService, SpectrumService>();
			services.AddScoped<ITokenService, TokenService>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IForgotPasswordService, ForgotPasswordService>();
			services.AddScoped<IAdminService, AdminService>();
			services.AddScoped<IUserService ,UserService>();
			return services;
		}
	}
}
