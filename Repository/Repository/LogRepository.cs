
namespace Repository.Repository
{
	public class LogRepository : ILogRepository
	{
		private readonly CannabisAccessorriesDBContext _context;
		public LogRepository(CannabisAccessorriesDBContext context)
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
