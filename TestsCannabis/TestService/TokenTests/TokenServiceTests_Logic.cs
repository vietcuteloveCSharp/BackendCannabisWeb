using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace TestsCannabis.TestService.TokenServiceTests
{
	public class TokenServiceTests_Logic
	{
		private TokenService CreateTokenService(JwtSettings jwtSettings)
		{
			var options = Options.Create(jwtSettings);
			var mockConfig = new Mock<IConfiguration>();
			return new TokenService(options, mockConfig.Object);
		}
		[Fact]
		public void GenerateAccessToken_ShouldContainExpectedClaimsAndExpiry()
		{
			// Arrange
			var jwtSettings = new JwtSettings
			{
				Key = "super_secret_key_for_testing_purposes_!@#1234567890",
				Issuer = "test-issuer",
				Audience = "test-audience",
				AccessTokenLifetimeMinutes = 10
			};
			var svc = CreateTokenService(jwtSettings);

			var payload = new TokenPayload
			{
				UserId = "42",
				UserName = "tester",
				Role = "Admin"
			};

			// Act
			var tokenString = svc.GenerateAccessToken(payload);

			// Assert
			var handler = new JwtSecurityTokenHandler();
			var jwt = handler.ReadJwtToken(tokenString);

			Assert.Equal("42", jwt.Claims.First(c => c.Type == JwtRegisteredClaimNames.Sub).Value);
			Assert.Equal("tester", jwt.Claims.First(c => c.Type == JwtRegisteredClaimNames.UniqueName).Value);
			Assert.Equal("Admin", jwt.Claims.First(c => c.Type == ClaimTypes.Role).Value);
			Assert.Equal("test-issuer", jwt.Issuer);
			Assert.Contains("test-audience", jwt.Audiences);

			// Expiry
			var expTime = jwt.ValidTo.ToUniversalTime();
			var expected = DateTime.UtcNow.AddMinutes(jwtSettings.AccessTokenLifetimeMinutes);
			var diffSeconds = Math.Abs((expTime - expected).TotalSeconds);
			Assert.True(diffSeconds < 60, $"Expiry difference too big: {diffSeconds} seconds");
		}

		[Fact]
		public void ValidateToken_ShouldReturnPrincipal_WhenTokenIsValid()
		{
			// Arrange
			var jwtSettings = new JwtSettings
			{
				Key = "super_secret_key_for_testing_purposes_!@#1234567890",
				Issuer = "test-issuer",
				Audience = "test-audience",
				AccessTokenLifetimeMinutes = 5
			};
			var svc = CreateTokenService(jwtSettings);

			var payload = new TokenPayload { UserId = "7", UserName = "alice", Role = "User" };
			var token = svc.GenerateAccessToken(payload);

			// Act
			var principal = svc.ValidateToken(token);

			// Assert
			Assert.NotNull(principal);
			Assert.Equal("7", principal!.FindFirst(ClaimTypes.NameIdentifier)!.Value);
			Assert.Equal("alice", principal.Identity?.Name);
		}

		[Fact]
		public void GetPrincipalFromExpiredToken_ShouldReturnPrincipal_WhenTokenExpired()
		{
			// Arrange
			var key = "same_key_for_both_services_987654321";
			var normalSettings = new JwtSettings
			{
				Key = key,
				Issuer = "iss",
				Audience = "aud",
				AccessTokenLifetimeMinutes = 5
			};
			var expiredSettings = new JwtSettings
			{
				Key = key,
				Issuer = "iss",
				Audience = "aud",
				AccessTokenLifetimeMinutes = -1 // already expired
			};

			var normalSvc = CreateTokenService(normalSettings);
			var expiredSvc = CreateTokenService(expiredSettings);

			var payload = new TokenPayload { UserId = "100", UserName = "expired-user", Role = "User" };
			var expiredToken = expiredSvc.GenerateAccessToken(payload);

			// Act
			var principal = normalSvc.GetPrincipalFromExpiredToken(expiredToken);

			// Assert
			Assert.NotNull(principal);
			Assert.Equal("expired-user", principal!.Identity?.Name);
		}

		[Fact]
		public void ValidateToken_ShouldReturnNull_WhenTokenTampered()
		{
			// Arrange
			var jwtSettings = new JwtSettings
			{
				Key = "completely_different_hmacsha256_key_654321",
				Issuer = "iss",
				Audience = "aud",
				AccessTokenLifetimeMinutes = 5
			};
			var svc = CreateTokenService(jwtSettings);
			var payload = new TokenPayload { UserId = "1", UserName = "bob", Role = "User" };

			var token = svc.GenerateAccessToken(payload);

			// Tamper token
			var tampered = token + "x";

			// Act
			var principal = svc.ValidateToken(tampered);

			// Assert
			Assert.Null(principal);
		}
	}
}
