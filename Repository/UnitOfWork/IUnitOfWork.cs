
namespace Repository.UnitOfWork
{
	public interface IUnitOfWork : IDisposable
	{
		public IAddressRepository Addresses { get; }
		public ICategoryRepository Categories { get; }
		public IProductRepository Products { get; }
		public IGrowLightRepository GrowLights { get; }
		public IGrowTentRepository GrowTents { get; }
		public ICarbonFilterRepository CarbonFilters { get; }
		public IDehumidifierRepository Dehumidifiers { get; }
		public ISeedRepository Seeds { get; }
		public INutrientRepository Nutrients{ get; }
		public IUserRepository Users { get; }
		public IBrandRepository Brands { get; }
		public IBreederRepository Breeders { get; }
		public IChipModelRepository ChipModels { get; }
		public IClassificationRepository Classifications { get; }
		public ICoolingSystemRepository CoolingSystems { get; }
		public INutrientTypeRepository NutrientTypes { get; }
		public IPowerSupplyRepository PowerSupplies { get; }		
		public IRefreshTokenRepository RefreshTokens { get; }
		public IRoleRepository Roles { get; }
		public ISpectrumRepository Spectrums { get; }
		public IAuditLogRepository AuditLogs { get; }
		Task<int> SaveChangesAsync();
		Task BeginTransactionAsync();
		Task CommitTransactionAsync();
		Task RollbackTransactionAsync();
		Task DisposeTransactionAsync();
	
	}
}
