using DAL.Repository.Implementations.Internal;
using DAL.Repository.Implementations.Shop;
using DAL.Repository.Interfaces.Internal;
using DAL.Repository.Interfaces.Shop;

namespace Cannabis.Server.DependencyInjection
{
	public static class RepositoryExtensions
	{
		public static IServiceCollection AddApplicationRepositories(this IServiceCollection services)
		{
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped<IAddressRepository, AddressRepository>();
			services.AddScoped<IBrandRepository, BrandRepository>();
			services.AddScoped<IRoleRepository, RoleRepository>();
			return services;
		}
	}
}
