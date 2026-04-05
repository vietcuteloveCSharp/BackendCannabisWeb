using DAL.Entities.Audit;

namespace Service.IServices.IServiceLogger
{
	public interface IAuditLoggerService
	{
		Task<IEnumerable<AuditLog>> GetAllAsync();
		Task<IEnumerable<AuditLog>> GetByUserIdAsync(int userId);
		Task<IEnumerable<AuditLog>> GetByTableAsync(string tableName);
		Task AddAuditLogAsync(AuditLog auditLog);
	}
}
