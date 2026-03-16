
namespace Repository.Repository
{
	public class LogRepository : ILogRepository
	{
		private readonly CannabisAccessoriesDBContext _context;
		public LogRepository(CannabisAccessoriesDBContext context)
		{
			this._context = context;
		}
		public async Task<AuditLog> AddLogAsync(AuditLog log)
		{
			_context.AuditLogs.Add(log);
			await _context.SaveChangesAsync();
			return log;
		}
	}
}
