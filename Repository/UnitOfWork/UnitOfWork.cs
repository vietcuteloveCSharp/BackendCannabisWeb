using DAL.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace Repository.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		private IDbContextTransaction? _transaction;

		private readonly CannabisAccessoriesDBContext _context;
		private Dictionary<string, object>? _repositories;

		private IProductRepository? _productsRepository;

		private IGrowLightRepository? _growLightRepository;

		private IGrowTentRepository? _growTentRepository;

		private ICarbonFilterRepository? _carbonFilterRepository;

		private IDehumidifierRepository? _dehumidifierRepository;

		public ISeedRepository? _seedRepository;

		private INutrientRepository? _nutrientRepository;

		private IAddressRepository? _addressRepository;

		private IUserRepository? _userRepository;

		private IBrandRepository? _brandRepository;

		private IBreederRepository? _breederRepository;

		private IChipModelRepository? _chipModelRepository;

		private IClassificationRepository? _classificationRepository;

		private ICoolingSystemRepository? _coolingSystemRepository;

		private INutrientTypeRepository? _nutrientTypeRepository;

		private IPowerSupplyRepository? _powerSupplyRepository;

		private IRefreshTokenRepository? _refreshTokenRepository;

		private IRoleRepository? _roleRepository;

		private ISpectrumRepository? _spectrumRepository;


		private ICategoryRepository? _categoryRepository;

		public UnitOfWork(CannabisAccessoriesDBContext context, IAuditLogRepository auditLogger)
		{
			_context = context;
			AuditLogs = auditLogger;

		}
		public IProductRepository Products => _productsRepository ??= new ProductRepository(_context);
		public ICategoryRepository Categories => _categoryRepository ??= new CategoryRepository(_context);
		public ISpectrumRepository Spectrums => _spectrumRepository ??= new SpectrumRepository(_context);
		public IUserRepository Users => _userRepository ??= new UserRepository(_context);
		public IAddressRepository Addresses => _addressRepository ??= new AddressRepository(_context);
		public IGrowLightRepository GrowLights => _growLightRepository ??= new GrowLightRepository(_context);
		public IGrowTentRepository GrowTents => _growTentRepository ??= new GrowTentRepository(_context);
		public ICarbonFilterRepository CarbonFilters => _carbonFilterRepository ??= new CarbonFilterRepository(_context);
		public IDehumidifierRepository Dehumidifiers => _dehumidifierRepository ??= new DehumidifierRepository(_context);
		public ISeedRepository Seeds => _seedRepository ??= new SeedRepository(_context);
		public INutrientRepository Nutrients => _nutrientRepository ??= new NutrientRepository(_context);
		public IBrandRepository Brands => _brandRepository ??= new BrandRepository(_context);
		public IBreederRepository Breeders => _breederRepository ??= new BreederRepository(_context);
		public IChipModelRepository ChipModels => _chipModelRepository ??= new ChipModelRepository(_context);
		public IClassificationRepository Classifications => _classificationRepository ??= new ClassificationRepository(_context);
		public ICoolingSystemRepository CoolingSystems => _coolingSystemRepository ??= new CoolingSystemRepository(_context);
		public INutrientTypeRepository NutrientTypes => _nutrientTypeRepository ??= new NutrientTypeRepository(_context);
		public IPowerSupplyRepository PowerSupplies => _powerSupplyRepository ??= new PowerSupplyRepository(_context);
		public IRefreshTokenRepository RefreshTokens => _refreshTokenRepository ??= new RefreshTokenRepository(_context);
		public IRoleRepository Roles => _roleRepository ??= new RoleRepository(_context);

		public IAuditLogRepository AuditLogs { get; }

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
		public async Task<IDbContextTransaction> BeginTransactionAsync()
		{
			return await _context.Database.BeginTransactionAsync();
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

		public IBaseRepository<TEntity> Repository<TEntity>() where TEntity : class
		{
			_repositories ??= new Dictionary<string, object>();

			var type = typeof(TEntity).Name;

			if (!_repositories.ContainsKey(type))
			{
				// Khởi tạo BaseRepository cho Entity tương ứng
				var repositoryInstance = new BaseRepository<TEntity>(_context);
				_repositories.Add(type, repositoryInstance);
			}

			return (IBaseRepository<TEntity>)_repositories[type]!;
		}
		

	}
}
