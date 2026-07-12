using DAL.Entities.Internal;

namespace DAL.Repository.Implementations.Internal
{
	public class StaffSessionRepository : BaseRepository<StaffSession>, IStaffSessionRepository
	{
		public StaffSessionRepository(CannabisAccessoriesDBContext context) : base(context)
		{
		}

		public async Task<StaffSession?> GetByTokenAsync(SessionTokenRequest request)
		{
			return await _context.StaffSessions
				.FirstOrDefaultAsync(s => s.SessionToken == request.SessionToken && s.IsDeleted == false);
		}

		public async Task<List<StaffSession>> GetActiveSessionsByStaffIdAsync(GetSessionTokenRequest request)
		{
			var query = GetQueryable(request.TrackChanges);
			return await query
				.Where(s => s.StaffId == request.StaffId
						 && s.ExpiresAt > DateTime.UtcNow
						 && s.IsDeleted == false)
				.ToListAsync();
		}

		public async Task<bool> DeleteAsync(SessionTokenRequest request)
		{
			// Tìm session dựa theo Token vừa nạp chồng ở trên
			var session = await GetByTokenAsync(request);
			if (session == null) return false;
			session.IsDeleted = true;
			session.DeletedAt = DateTime.UtcNow;
			Update(session);
			return true;
		}
	}
}
