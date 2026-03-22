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
		public string GenerateAccessToken(IEnumerable<Claim> claims)
		{
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			//var claims = new List<Claim>
			//{
			//	new Claim(JwtRegisteredClaimNames.Sub, payload.UserId.ToString()),
			//	new Claim(JwtRegisteredClaimNames.UniqueName,payload.UserName),
			//	new Claim(ClaimTypes.Name, payload.UserName),
			//	new Claim(ClaimTypes.Role, payload.Role),
			//	new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
			//	new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString())
			//};
			var expires = _jwtSettings.AccessTokenLifetimeSeconds;
			// Bảo vệ trường hợp lifetime âm hoặc bằng 0
			if (expires <= 0)
			{
				// Giúp token có hiệu lực hợp lệ trong 1 giây (để tránh IDX12401)
				expires = 1;
			
			}
			// Tạo token
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.UtcNow.AddSeconds(_jwtSettings.AccessTokenLifetimeSeconds),
				Issuer = _jwtSettings.Issuer,
				Audience = _jwtSettings.Audience,
				SigningCredentials = creds,
				NotBefore = DateTime.UtcNow
			};

			var tokenHandler = new JwtSecurityTokenHandler();
			var token = tokenHandler.CreateToken(tokenDescriptor);

			return tokenHandler.WriteToken(token);
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
					ValidateLifetime = true,
					ClockSkew = TimeSpan.FromMinutes(5)// allow expired token
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
