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


		private IAddressRepository? _addressRepository;

		private IUserRepository? _userRepository;

		private IBrandRepository? _brandRepository;


		private IRefreshTokenRepository? _refreshTokenRepository;

		private IRoleRepository? _roleRepository;



		private ICategoryRepository? _categoryRepository;

		public UnitOfWork(CannabisAccessoriesDBContext context, IAuditLogRepository auditLogger)
		{
			_context = context;
			AuditLogs = auditLogger;

		}
		public IProductRepository Products => _productsRepository ??= new ProductRepository(_context);
		public ICategoryRepository Categories => _categoryRepository ??= new CategoryRepository(_context);
		public IUserRepository Users => _userRepository ??= new UserRepository(_context);
		public IAddressRepository Addresses => _addressRepository ??= new AddressRepository(_context);
		public IBrandRepository Brands => _brandRepository ??= new BrandRepository(_context);		public IRefreshTokenRepository RefreshTokens => _refreshTokenRepository ??= new RefreshTokenRepository(_context);
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
			// Tạo chiến lược thực thi (đã cấu hình Retry trong Program.cs)
			var strategy = _context.Database.CreateExecutionStrategy();

			// Lưu ý: IDbContextTransaction không thể khởi tạo trực tiếp bên ngoài strategy
			// khi dùng Retry. Nhưng ta có thể dùng mẹo hoặc bọc lại như sau:

			_transaction= await strategy.ExecuteAsync(async () =>
			{
				return await _context.Database.BeginTransactionAsync();
			});
			return _transaction;
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
		// thực thi trong trasaction
		public async Task ExecuteInTransactionAsync(Func<Task> action)
		{
			var strategy = _context.Database.CreateExecutionStrategy();
			await strategy.ExecuteAsync(async () =>
			{// Toàn bộ quy trình mở - làm - lưu - đóng nằm gọn trong 1 lần thực thi
				using var transaction = await _context.Database.BeginTransactionAsync();
				try
				{
					await action(); // Chạy toàn bộ logic truyền từ Service vào
					await _context.SaveChangesAsync();
					await transaction.CommitAsync();
				}
				catch
				{
					await transaction.RollbackAsync();
					throw; // Quăng lỗi để Strategy thực hiện Retry
				}
			});
		}
	}
}
