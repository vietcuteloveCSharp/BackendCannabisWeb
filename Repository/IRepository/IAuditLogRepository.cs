
namespace Repository.IRepository
{
	public interface IAuditLogRepository : IBaseRepository<AuditLog>	
	{
		Task<IEnumerable<AuditLog>> GetByUserIdAsync(int userId);
		Task<IEnumerable<AuditLog>> GetByTableAsync(string tableName);
	}
}
