
using Microsoft.EntityFrameworkCore.Storage;

namespace Repository.UnitOfWork
{
	public interface IUnitOfWork : IDisposable
	{
		public IAddressRepository Addresses { get; }
		public ICategoryRepository Categories { get; }
		public IProductRepository Products { get; }
		public IUserRepository Users { get; }
		public IBrandRepository Brands { get; }
		public IRefreshTokenRepository RefreshTokens { get; }
		public IRoleRepository Roles { get; }
		public IAuditLogRepository AuditLogs { get; }
		IBaseRepository<TEntity> Repository<TEntity>() where TEntity : class;
		Task<int> SaveChangesAsync();
		Task<IDbContextTransaction> BeginTransactionAsync();
		Task CommitTransactionAsync();
		Task RollbackTransactionAsync();
		Task DisposeTransactionAsync();
		Task ExecuteInTransactionAsync(Func<Task> action);

	}
}
