namespace Cannabis.Server.DependencyInjection{
	public static class ServiceExtenstions
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
		
			services.AddScoped<IAuthService, AuthService>();
			services.AddScoped<IBrandService, BrandService>();	
			services.AddScoped<ICategoryService, CategoryService>();
			services.AddScoped<IRefreshTokenService, RefreshTokenService>();
			services.AddScoped<ITokenService, TokenService>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IForgotPasswordService, ForgotPasswordService>();
			services.AddScoped<IAdminService, AdminService>();
			services.AddScoped<IUserService ,UserService>();
			services.AddScoped<IRoleService, RoleService>();
			services.AddScoped<IUserStatusService, UserStatusService>();
			return services;
		}
	}
}
