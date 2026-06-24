
namespace Service.Interfaces.Auth
{
	public interface IAuthService
	{
		Task<TokenDTO> LoginAsync(LoginResquestDTO loginResquestDTO); // login return access token and refresh token
		Task LogoutAsync(int userId,string refreshTokenValue); // logout and revoke all refresh tokens
	}
}
