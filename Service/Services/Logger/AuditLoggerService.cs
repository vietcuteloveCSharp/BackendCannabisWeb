using Service.IServices.IServiceLogger;

namespace Service.Services.ServicesLogger
{
	internal class AuditLoggerService : IAuditLoggerService
	{
		private readonly IUnitOfWork _unitOfWork;
		public AuditLoggerService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task AddAuditLogAsync(AuditLog auditLog)
		{
			await _unitOfWork.AuditLogs.AddAsync(auditLog);
			await _unitOfWork.SaveChangesAsync();
		}

		public async Task<IEnumerable<AuditLog>> GetAllAsync()
		{
			return await _unitOfWork.AuditLogs.GetAllAsync();
		}

		public async Task<IEnumerable<AuditLog>> GetByTableAsync(string tableName)
		{
			return await _unitOfWork.AuditLogs.GetByTableAsync(tableName);
		}

		public async Task<IEnumerable<AuditLog>> GetByUserIdAsync(int userId)
		{
			return await _unitOfWork.AuditLogs.GetByUserIdAsync(userId);
		}
	}
}
