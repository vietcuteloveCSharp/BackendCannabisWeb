namespace Service.IServices.IServicesAuth
{
	public interface IAuthService
	{
		Task<TokenDTO> LoginAsync(LoginResquestDTO loginResquestDTO); // login return access token and refresh token
		Task<UserDTO> RegisterUserAsync(CreateUserDTO createUserDTO); //register account
		Task LogoutAsync(int userId,string refreshTokenValue); // logout and revoke all refresh tokens
	}
}
