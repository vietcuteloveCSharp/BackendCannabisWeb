namespace TestsCannabis.TestService.TokenServiceTests
{
	public class TokenServiceTests_EdgeCases
	{
		private TokenService CreateTokenService(JwtSettings jwtSettings)
		{
			var options = Options.Create(jwtSettings);
			var mockConfig = new Mock<IConfiguration>();
			return new TokenService(options, mockConfig.Object);
		}

		[Fact]
		public void GenerateAccessToken_NullPayload_ShouldThrow()
		{
			// Arrange
			var jwtSettings = new JwtSettings
			{
				Key = "null_payload_key_123",
				Issuer = "iss",
				Audience = "aud",
				AccessTokenLifetimeMinutes = 5
			};
			var svc = CreateTokenService(jwtSettings);

			// Act & Assert: current implementation will throw (null deref). We assert that an exception is thrown.
			Assert.ThrowsAny<Exception>(() => svc.GenerateAccessToken(null!));
		}

		[Fact]
		public void ValidateToken_ShouldReturnNull_WhenDifferentKeySigned()
		{
			// Arrange
			var originalSettings = new JwtSettings
			{
				Key = "original_key_for_hmacsha256_test_123456",
				Issuer = "iss",
				Audience = "aud",
				AccessTokenLifetimeMinutes = 10
			};
			var otherSettings = new JwtSettings
			{
				Key = "completely_different_hmacsha256_key_654321",
				Issuer = "iss",
				Audience = "aud",
				AccessTokenLifetimeMinutes = 10
			};

			var svc = CreateTokenService(originalSettings);
			var otherSvc = CreateTokenService(otherSettings);

			var payload = new TokenPayload { UserId = "9", UserName = "charlie", Role = "User" };

			// token signed with other key
			var otherToken = otherSvc.GenerateAccessToken(payload);

			// Act
			var principal = svc.ValidateToken(otherToken);

			// Assert - signature mismatch -> validation fails
			Assert.Null(principal);
		}

		[Fact]
		public void GetPrincipalFromExpiredToken_ShouldReturnNull_WhenSignatureInvalid()
		{
			// Arrange
			var goodKey = "good_key_for_hmacsha256_testing_123456"; // 36 chars
			var badKey = "bad_key_for_hmacsha256_testing_654321";   // khác key

			var svcGood = CreateTokenService(new JwtSettings
			{
				Key = goodKey,
				Issuer = "iss",
				Audience = "aud",
				AccessTokenLifetimeMinutes = 5
			});

			var svcBadExpired = CreateTokenService(new JwtSettings
			{
				Key = badKey, // different key (so signature invalid for svcGood)
				Issuer = "iss",
				Audience = "aud",
				AccessTokenLifetimeMinutes = -1 // expired
			});

			var payload = new TokenPayload { UserId = "55", UserName = "someone", Role = "User" };
			var badExpiredToken = svcBadExpired.GenerateAccessToken(payload);

			// Act: svcGood tries to read expired token but signature invalid -> should get null
			var principal = svcGood.GetPrincipalFromExpiredToken(badExpiredToken);

			// Assert
			Assert.Null(principal);
		}
	}

}
