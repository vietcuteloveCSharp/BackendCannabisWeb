namespace Service.Implementations.Shared
{
	public class TokenService : ITokenService
	{	private readonly IConfiguration _configuration;
		private readonly JwtSettings _jwtSettings;
		public TokenService(IOptions<JwtSettings> jwtSettings, IConfiguration configuration)
		{
			_jwtSettings = jwtSettings.Value;
			_configuration = configuration;
		}
		
		// Generate JWT token
		public string GenerateAccessToken(IEnumerable<Claim> claims)
		{

			// Kiểm tra an toàn: Nếu Key trống thì báo lỗi rõ ràng thay vì crash
			if (string.IsNullOrEmpty(_jwtSettings.Key))
			{
				throw new InvalidOperationException("JWT Key is not configured in appsettings.json or JwtSettings class.");
			}
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var expires = _jwtSettings.AccessTokenLifetimeMinutes ;
		
			// Tạo token
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.UtcNow.AddMinutes(expires),
				Issuer = _jwtSettings.Issuer,
				Audience = _jwtSettings.Audience,
				SigningCredentials = creds,
				NotBefore = DateTime.UtcNow.AddSeconds(-5)
			};

			var tokenHandler = new JwtSecurityTokenHandler();
			var token = tokenHandler.CreateToken(tokenDescriptor);

			return tokenHandler.WriteToken(token);
		}
		///// <summary>
		///  Đọc Claims từ token đã hết hạn để phục vụ luồng xoay vòng Refresh Token
		/// </summary>
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
					ValidateIssuer = true,
					ValidIssuer=_jwtSettings.Issuer,
					ValidateAudience = true,
					ValidAudience = _jwtSettings.Audience,
					ValidateLifetime = false,
					ClockSkew = TimeSpan.Zero
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
