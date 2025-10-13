namespace Repository.IRepository
{
	public interface IRefreshTokenRepository
	{
		Task AddAsync(RefreshToken refreshToken);
		Task<RefreshToken?> GetByTokenAsync(string refreshToken, bool includeRevoked = false);
		Task<List<RefreshToken>> GetByUserIdAsync(int userId, bool onlyActive = true);
		Task<RefreshToken?> GetLatestByUserIdAsync(int userId, bool onlyActive = true);
		Task<int> RevokeAllAsync(int userId);
		Task<bool> RevokeTokenAsync(string token);// (tuỳ chọn) Đăng xuất toàn bộ thiết bị
		Task UpdateAsync(RefreshToken refreshToken);
		Task<bool> ExistsAsync(string refreshToken);

	}
}
