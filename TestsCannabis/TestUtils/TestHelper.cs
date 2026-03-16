
namespace TestsCannabis.TestUtils
{
	public static class TestHelper
	{
		public static readonly IOptions<JwtSettings> JwtSettings = Options.Create(new JwtSettings
		{
			Key = "test-secret-key-1234567890",
			Issuer = "TestIssuer",
			Audience = "TestAudience",
			AccessTokenLifetimeSecond = TimeSpan.FromSeconds(3600),
			RefreshTokenLifetimeDays = 7
		});
	}
}
