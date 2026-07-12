namespace Service.Interfaces.Shared
{
	public interface ITokenService
	{
		string GenerateAccessToken(IEnumerable<Claim> claims); // gen JWT access token
		ClaimsPrincipal? ValidateToken(string token);
		ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
	}
}
