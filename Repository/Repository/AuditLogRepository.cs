namespace Repository.Repository
{
	public class AuditLogRepository : BaseRepository<AuditLog>, IAuditLogRepository
	{
		public AuditLogRepository(CannabisAccessorriesDBContext context) : base(context)
		{
		}
		public async Task<IEnumerable<AuditLog>> GetByTableAsync(string tableName)
		{
			return await _context.AuditLogs
			   .Where(x => x.TableName == tableName)
			   .OrderByDescending(x => x.CreatedAt)
			   .ToListAsync();
		}

		public async Task<IEnumerable<AuditLog>> GetByUserIdAsync(int userId)
		{
			return await _context.AuditLogs
				   .Where(x => x.UserId == userId)
				   .OrderByDescending(x => x.CreatedAt)
				   .ToListAsync();
		}
	}
}
