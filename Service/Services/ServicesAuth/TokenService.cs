using Service.Helpers;

namespace Service.Service.ServicesAuth
{
	public class TokenService : ITokenService
	{	private readonly IConfiguration _configuration;
		private readonly JwtSettings _jwtSettings;
		public TokenService(IOptions<JwtSettings> jwtSettings, IConfiguration configuration)
		{
			_jwtSettings = jwtSettings.Value;
			this._configuration = configuration;
		}
		
		// Generate JWT token
		public string GenerateAccessToken(TokenPayload payload )
		{
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, payload.UserId),
				new Claim(ClaimTypes.Name,payload.UserName),
				new Claim(ClaimTypes.Role, payload.Role)
			};
			
			
			// Tạo claims
			
			var token = new JwtSecurityToken(
				issuer: _jwtSettings.Issuer,
				audience: _jwtSettings.Audience,
				claims: claims,
				expires: DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenLifetimeMinutes),
				signingCredentials: creds
			);
			var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
			return tokenString;
		}

		public ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
		{
			if (string.IsNullOrEmpty(token)) return null;

			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.UTF8.GetBytes(_jwtSettings.Key);

			try
			{
				return tokenHandler.ValidateToken(token, new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = false,
					ValidateAudience = false,
					ValidateLifetime = false // allow expired token
				}, out _);
			}
			catch
			{
				return null;
			}
		}

		// Validate JWT token and return ClaimsPrincipal
		public ClaimsPrincipal? ValidateToken(string token)
		{
			return JwtHelper.TryValidateToken(token, _jwtSettings, out var principal) ? principal : null;
		}
	}
}
