namespace Service.Helpers
{
	public static class JwtHelper
	{
		public static bool TryValidateToken(string token, JwtSettings jwtSettings,out ClaimsPrincipal? claimsPrincipal)
		{
			claimsPrincipal = null;
			if(string.IsNullOrEmpty(token)) return false;
			var tokenHandler = new JwtSecurityTokenHandler();
			if(!tokenHandler.CanReadToken(token)) return false;

			var key = Encoding.UTF8.GetBytes(jwtSettings.Key);
			var validationParameters = new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(key),
				ValidateIssuer = false,
				ValidateAudience = false,
				ClockSkew = TimeSpan.Zero
			};
			try
			{
				claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out _);
				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}
