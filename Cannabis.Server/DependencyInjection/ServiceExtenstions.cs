using Shared.Implementations.Audit;
using Shared.Interfaces.Audit;
using Service.Interfaces.Internal;
using Service.Implementations.Internal;
using Service.Interfaces.Auth.Internal;
using Service.Implementations.Auth.Internal;

namespace Cannabis.Server.DependencyInjection{
	public static class ServiceExtenstions
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
		
			services.AddScoped<IBrandService, BrandService>();	
			services.AddScoped<ICategoryService, CategoryService>();
			services.AddScoped<ITokenService, TokenService>();
			services.AddScoped<IRoleService, RoleService>();
			services.AddScoped<IStaffAuthService, StaffAuthService>();
			services.AddScoped<IStaffRefreshTokenService, StaffRefreshTokenService>();
			
			return services;
		}
	}
}
