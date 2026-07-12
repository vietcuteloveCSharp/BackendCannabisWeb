
using DAL.Repository.Implementations.Internal;
using DAL.Repository.Implementations.Shop;
using DAL.Repository.Interfaces.Internal;
using DAL.Repository.Interfaces.Shop;
using Microsoft.EntityFrameworkCore;

namespace Repository.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		private IDbContextTransaction? _transaction;

		private readonly CannabisAccessoriesDBContext _context;
		private Dictionary<string, object>? _repositories;
		// Khai báo các backing field cho các repo mới tách
		private IStaffRepository? _staffs;
		private IStaffSessionRepository?_staffSessions;
		private IStaffRefreshTokenRepository? _staffRefreshTokens;
		private IStaffStatusRepository? _staffStatuses;
		private IRoleRepository? _roles;

		private ICustomerRepository? _customers;
		private ICustomerSessionRepository? _customerSessions;
		private ICustomerRefreshTokenRepository? _customerRefreshTokens;
		private IAddressRepository? _addresses;
		


		private IBrandRepository? _brandRepository;


	


		public UnitOfWork(CannabisAccessoriesDBContext context)
		{
			_context = context;


		}
		// Triển khai cơ chế Lazy Loading (chỉ khởi tạo khi được gọi tới)
		public IStaffRepository Staffs => _staffs ??= new StaffRepository(_context);
		public IStaffSessionRepository StaffSessions => _staffSessions ??= new StaffSessionRepository(_context);
		public IStaffRefreshTokenRepository StaffRefreshTokens => _staffRefreshTokens ??= new StaffRefreshTokenRepository(_context);
		public IStaffStatusRepository StaffStatuses => _staffStatuses ??= new StaffStatusRepository(_context);
		public IRoleRepository Roles => _roles ??= new RoleRepository(_context);
		public ICustomerRepository Customers => _customers ??= new CustomerRepository(_context);
		public ICustomerSessionRepository CustomerSessions => _customerSessions ??= new CustomerSessionRepository(_context);
		public ICustomerRefreshTokenRepository CustomerRefreshTokens => _customerRefreshTokens ??= new CustomerRefreshTokenRepository(_context);
		public IAddressRepository Addresses => _addresses ??= new AddressRepository(_context);

		public IBrandRepository Brands => _brandRepository ??= new BrandRepository(_context);			




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
