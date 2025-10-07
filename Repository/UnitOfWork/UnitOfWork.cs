using Microsoft.EntityFrameworkCore.Storage;

namespace Repository.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		private IDbContextTransaction? _transaction;

		private readonly CannabisAccessorriesDBContext _context;
		public IProductRepository Products { get; private set; }

		public IGrowLightRepository GrowLights { get; private set; }

		 public IGrowTentRepository GrowTents { get; private set; }

		public ICarbonFilterRepository CarbonFilters { get; private set; }

		public IDehumidifierRepository Dehumidifiers { get; private set; }

		public ISeedRepository Seeds { get; private set; }

		public INutrientRepository Nutrients { get;private set; }

		public IAddressRepository Addresses { get; private set; }
		public IUserRepository Users { get; private set; }

		public IBrandRepository Brands { get; private set; }
		public IBreederRepository Breeders { get; private set; }
		public IChipModelRepository ChipModels { get; private set; }
		public IClassificationRepository Classifications { get; private set; }
		public ICoolingSystemRepository CoolingSystems { get; private set; }

		public INutrientTypeRepository NutrientTypes { get; private set; }
		public IPowerSupplyRepository PowerSupplies { get; private set; }

		public IRefreshTokenRepository RefreshTokens { get; private set; }

		public IRoleRepository Roles { get; private set; }

		public ISpectrumRepository Spectrums {get; private set;}

		public IAuditLogRepository AuditLogs { get; private set; }

		public UnitOfWork(CannabisAccessorriesDBContext context, IAuditLogRepository auditLogger)
		{
			_context = context;
			Products = new ProductRepository(_context);
			CarbonFilters = new CarbonFilterRepository(_context);
			Dehumidifiers = new DehumidifierRepository(_context);
			GrowLights = new GrowLightRepository(_context);
			Nutrients = new NutrientRepository(_context);
			GrowTents = new GrowTentRepository(_context);
			Seeds = new SeedRepository(_context);
			Addresses = new AddressRepository(_context);
			Users = new UserRepository(_context);
			Brands = new BrandRepository(_context);
			ChipModels = new ChipModelRepository(_context);
			Breeders = new BreederRepository(_context);
			Classifications = new ClassificationRepository(_context);
			CoolingSystems = new CoolingSystemRepository(_context);
			NutrientTypes = new NutrientTypeRepository(_context);
			PowerSupplies = new PowerSupplyRepository(_context);
			RefreshTokens = new RefreshTokenRepository(_context);
			Roles = new RoleRepository(_context);
			Spectrums = new SpectrumRepository(_context);
			AuditLogs = auditLogger;

		}

		
		public void Dispose()
		{
			_context.Dispose();
			GC.SuppressFinalize(this); //optional, if you have a finalizer
		}
		//huỷ tất cả nếu có lỗi xảy ra
		public void Rollback()
		{
			_transaction?.Rollback();
		}

		public async Task<int> SaveChangesAsync()
		{
			return await _context.SaveChangesAsync();
		}
		//bắt đầu 1 transaction với database
		public async Task BeginTransactionAsync()
		{
			_transaction = await _context.Database.BeginTransactionAsync();
		}
		//xác nhận transaction và lưu thay đổi vào database
		public async Task CommitAsync()
		{
			if (_transaction != null)
			{
				await _transaction.CommitAsync();
				await _transaction.DisposeAsync();
			}
		}
	}
}
