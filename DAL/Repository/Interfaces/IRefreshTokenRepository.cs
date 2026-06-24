namespace DAL.Repository.Interfaces
{
	public interface IRefreshTokenRepository
	{
		Task AddAsync(UserRefreshToken refreshToken);
		Task<UserRefreshToken?> GetByTokenAsync(string refreshToken, bool includeRevoked = false);
		Task<List<UserRefreshToken>> GetByUserIdAsync(int userId, bool onlyActive = true);
		Task<UserRefreshToken?> GetLatestByUserIdAsync(int userId, bool onlyActive = true);
		Task<int> RevokeAllAsync(int userId);
		Task<bool> RevokeTokenAsync(string token);// (tuỳ chọn) Đăng xuất toàn bộ thiết bị
		Task UpdateAsync(UserRefreshToken refreshToken);
		Task<bool> ExistsAsync(string refreshToken);

	}
}
