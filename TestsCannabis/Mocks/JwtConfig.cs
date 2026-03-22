namespace TestsCannabis.Mocks
{
	public class JwtConfig
	{
			public string Key { get; set; } = default!;
			public string Issuer { get; set; } = default!;
			public string Audience { get; set; } = default!;
			public int AccessTokenLifetimeSecond { get; set; }
			public TimeSpan AccessTokenTimeSpan => TimeSpan.FromSeconds(AccessTokenLifetimeSecond);
			public int RefreshTokenExpiryDays { get; set; }
			public TimeSpan RefreshTokenTimeSpan => TimeSpan.FromDays(RefreshTokenExpiryDays);
	}
}
