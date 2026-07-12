using DAL.Repository.Interfaces.Internal;
using DAL.Repository.Interfaces.Shop;

namespace Repository.UnitOfWork
{
	public interface IUnitOfWork : IDisposable
	{
		// --- PHÂN HỆ NỘI BỘ QUẢN TRỊ (Internal Schema Repositories) ---
		public IStaffRepository Staffs { get; }               
		public IStaffSessionRepository StaffSessions { get; }  
		public IStaffRefreshTokenRepository StaffRefreshTokens { get; } 
		public IStaffStatusRepository StaffStatuses { get; }
		public IRoleRepository Roles { get; }
		// --- PHÂN HỆ KHÁCH HÀNG & MUA SẮM(Shop Schema Repositories) ---
        public ICustomerRepository Customers { get; }
		public ICustomerSessionRepository CustomerSessions { get; }
		public ICustomerRefreshTokenRepository CustomerRefreshTokens { get; }
		public IAddressRepository Addresses { get; }

		public IBrandRepository Brands { get; }
		IBaseRepository<TEntity> Repository<TEntity>() where TEntity : class;
		Task<int> SaveChangesAsync();
		Task<IDbContextTransaction> BeginTransactionAsync();
		Task CommitTransactionAsync();
		Task RollbackTransactionAsync();
		Task DisposeTransactionAsync();
		Task ExecuteInTransactionAsync(Func<Task> action);

	}
}
