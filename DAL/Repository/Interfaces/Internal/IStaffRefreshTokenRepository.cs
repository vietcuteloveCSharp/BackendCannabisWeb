



namespace DAL.Repository.Interfaces.Internal
{
	public interface IStaffRefreshTokenRepository : IBaseRepository<StaffRefreshToken>
	{
		//OVERLOAD
		Task<StaffRefreshToken?> GetByTokenAsync(string tokenHash);
		Task<StaffRefreshToken?> GetByTokenAsync(TokenQuery query);
		Task<bool> DeleteAsync(string tokenHash);
	}
}
