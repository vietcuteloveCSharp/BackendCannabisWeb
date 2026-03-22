namespace DTO.Options
{
	public class JwtSettings
	{
		public string Key { get; set; } = default!;
		public string Issuer { get; set; } = default!;
		public string Audience { get; set; } = default!;

		// Dùng int để nhận giá trị 3600 từ config
		public int AccessTokenLifetimeSeconds { get; set; }
		public int RefreshTokenExpiryDays { get; set; }
	}
}
