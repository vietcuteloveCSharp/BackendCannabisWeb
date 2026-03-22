using Microsoft.EntityFrameworkCore.Storage;

namespace Repository.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		private IDbContextTransaction? _transaction;

		private readonly CannabisAccessoriesDBContext _context;
		public IProductRepository Products { get; private set; }

		public IGrowLightRepository GrowLights { get; private set; }

		public IGrowTentRepository GrowTents { get; private set; }

		public ICarbonFilterRepository CarbonFilters { get; private set; }

		public IDehumidifierRepository Dehumidifiers { get; private set; }

		public ISeedRepository Seeds { get; private set; }

		public INutrientRepository Nutrients { get; private set; }

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

		public ISpectrumRepository Spectrums { get; private set; }

		public IAuditLogRepository AuditLogs { get; private set; }

		public ICategoryRepository Categories { get; private set; }

		public UnitOfWork(CannabisAccessoriesDBContext context, IAuditLogRepository auditLogger)
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
			Categories = new CategoryRepository(_context);
			AuditLogs = auditLogger;

		}

		// Cập nhật hàm Dispose chung của UnitOfWork
		public void Dispose()
		{
			_transaction?.Dispose(); // Giải phóng transaction nếu còn
			_context.Dispose();
			GC.SuppressFinalize(this);
		}

		public async Task<int> SaveChangesAsync()
		{
			return await _context.SaveChangesAsync();
		}

		//bắt đầu 1 transaction với database
		public async Task BeginTransactionAsync()
		{
			// Kiểm tra nếu đã có transaction rồi thì không tạo mới (Nested transaction handling)
			if (_transaction == null)
			{
				_transaction = await _context.Database.BeginTransactionAsync();
			}
		}
		// Xác nhận và Lưu
		public async Task CommitTransactionAsync()
		{
			try
			{
				//lưu trc khi commit
				await SaveChangesAsync();

				if (_transaction != null)
				{
					await _transaction.CommitAsync();
				}
			}
			catch (Exception)
			{
				await RollbackTransactionAsync();
				throw;
			}
			finally
			{
				await DisposeTransactionAsync();
			}
		}
		// Rollback bất đồng bộ
		public async Task RollbackTransactionAsync()
		{
			if (_transaction != null)
			{
				await _transaction.RollbackAsync();
				await DisposeTransactionAsync();
			}
		}
		// Hàm hỗ trợ giải phóng transaction
		public async Task DisposeTransactionAsync()
		{
			if (_transaction != null)
			{
				await _transaction.DisposeAsync();
				_transaction = null;
			}
		}
		// Cập nhật hàm Dispose chung của UnitOfWork
		
	}
}
