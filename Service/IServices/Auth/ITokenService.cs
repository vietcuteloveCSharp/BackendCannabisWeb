namespace Service.IServices.IServicesAuth
{
	public interface ITokenService
	{
		string GenerateAccessToken(TokenPayload payload); // gen JWT access token
		ClaimsPrincipal? ValidateToken(string token);
		ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
	}
}
