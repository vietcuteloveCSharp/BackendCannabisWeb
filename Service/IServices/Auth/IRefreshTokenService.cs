namespace Service.IServices.IServicesAuth
{
	public interface IRefreshTokenService
	{
		Task<UserRefreshToken> GenerateRefreshTokenAsync(int userId);
		Task StoreTokenAsync(RefreshTokenDTO refreshTokenDTO);
		Task<RefreshTokenDTO?> GetTokenAsync(string refreshTokenValue);
		Task<bool> ValidateRefreshTokenAsync(string refreshTokenValue);
		Task RevokeTokenAsync(string refreshTokenValue);
		Task<UserRefreshToken> ReplaceRefreshTokenAsync(int userId, string oldRefreshTokenValue);
		Task RevokeAllAsync(int userId);
	}
}
