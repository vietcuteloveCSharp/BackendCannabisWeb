namespace DTO.Options
{
	public class JwtSettings
	{
		public string Key { get; set; } = default!;
		public string Issuer { get; set; } = default!;
		public string Audience { get; set; } = default!;
		public int AccessTokenLifetimeMinutes { get; set; }
		public int RefreshTokenLifetimeDays { get; set; }
	}
}
