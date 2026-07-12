

namespace DAL.Repository.Interfaces.Internal
{
	public interface IStaffSessionRepository :IBaseRepository<StaffSession>
	{
		Task<StaffSession?> GetByTokenAsync(SessionTokenRequest request);
		Task<List<StaffSession>> GetActiveSessionsByStaffIdAsync(GetSessionTokenRequest request);
		Task<bool> DeleteAsync(SessionTokenRequest request);
	}
}
