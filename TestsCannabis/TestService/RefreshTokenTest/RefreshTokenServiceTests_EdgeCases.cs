
namespace TestsCannabis.TestService.RefreshTokenServiceTest
{
	public class RefreshTokenServiceTests_EdgeCases
	{
		private readonly Mock<IUnitOfWork> _uowMock;
		private readonly Mock<IMapper> _mapperMock;
		private readonly Mock<ITokenService> _tokenServiceMock;
		private readonly IOptions<JwtSettings> _jwtOptions;

		public RefreshTokenServiceTests_EdgeCases()
		{
			_uowMock = new Mock<IUnitOfWork>();
			_mapperMock = new Mock<IMapper>();
			_tokenServiceMock = new Mock<ITokenService>();

			var config = new ConfigurationBuilder()
				.AddJsonFile("appsettings.test.json", optional: false)
				.Build();

			var jwt = config.GetSection("Jwt").Get<JwtSettings>();
			_jwtOptions = Options.Create(jwt!);
		}

		private RefreshTokenService CreateService()
			=> new RefreshTokenService(
				_uowMock.Object,
				_mapperMock.Object,
				_tokenServiceMock.Object,
				_jwtOptions
			);

		// ---------------- NEGATIVE / EDGE CASES ---------------- //

		[Fact]
		public async Task GetTokenAsync_ShouldThrow_WhenTokenNotFound()
		{
			// Arrange
			var service = CreateService();
			_uowMock.Setup(x => x.RefreshTokens.GetByTokenAsync("missing_token",false))
					.ReturnsAsync((RefreshToken?)null);

			// Act & Assert
			await Assert.ThrowsAsync<UnauthorizedAccessException>(
				() => service.GetTokenAsync("missing_token")
			);
		}

		[Fact]
		public async Task GetTokenAsync_ShouldThrow_WhenTokenExpired()
		{
			// Arrange
			var service = CreateService();
			var expired = new RefreshToken
			{
				RefreshTokenValue = "expired",
				ExpiresAt = DateTime.UtcNow.AddDays(-1),
				IsRevoked = false
			};
			_uowMock.Setup(x => x.RefreshTokens.GetByTokenAsync("expired", false))
					.ReturnsAsync(expired);

			// Act & Assert
			await Assert.ThrowsAsync<UnauthorizedAccessException>(
				() => service.GetTokenAsync("expired")
			);
		}

		[Fact]
		public async Task GetTokenAsync_ShouldThrow_WhenTokenRevoked()
		{
			// Arrange
			var service = CreateService();
			var revoked = new RefreshToken
			{
				RefreshTokenValue = "revoked",
				ExpiresAt = DateTime.UtcNow.AddDays(1),
				IsRevoked = true
			};
			_uowMock.Setup(x => x.RefreshTokens.GetByTokenAsync("revoked", false))
					.ReturnsAsync(revoked);

			// Act & Assert
			await Assert.ThrowsAsync<UnauthorizedAccessException>(
				() => service.GetTokenAsync("revoked")
			);
		}

		[Fact]
		public async Task ReplaceRefreshTokenAsync_ShouldThrow_WhenTokenNotExist()
		{
			// Arrange
			var service = CreateService();
			_uowMock.Setup(x => x.RefreshTokens.GetByTokenAsync("old_token", true))
					.ReturnsAsync((RefreshToken?)null);

			// Act & Assert
			await Assert.ThrowsAsync<UnauthorizedAccessException>(
				() => service.ReplaceRefreshTokenAsync(10, "old_token")
			);
		}

		[Fact]
		public async Task ReplaceRefreshTokenAsync_ShouldThrow_WhenUserIdMismatch()
		{
			// Arrange
			var service = CreateService();
			var token = new RefreshToken
			{
				RefreshTokenValue = "old_token",
				UserId = 5,
				IsRevoked = false
			};
			_uowMock.Setup(x => x.RefreshTokens.GetByTokenAsync("old_token", true))
					.ReturnsAsync(token);

			// Act & Assert
			await Assert.ThrowsAsync<UnauthorizedAccessException>(
				() => service.ReplaceRefreshTokenAsync(999, "old_token")
			);
		}

		[Fact]
		public async Task ValidateRefreshTokenAsync_ShouldReturnFalse_WhenNullToken()
		{
			// Arrange
			var service = CreateService();

			// Act
			var result = await service.ValidateRefreshTokenAsync(null!);

			// Assert
			Assert.False(result);
		}

		[Fact]
		public async Task ValidateRefreshTokenAsync_ShouldReturnFalse_WhenRevoked()
		{
			// Arrange
			var service = CreateService();
			var token = new RefreshToken
			{
				RefreshTokenValue = "revoked",
				ExpiresAt = DateTime.UtcNow.AddDays(1),
				IsRevoked = true
			};
			_uowMock.Setup(x => x.RefreshTokens.GetByTokenAsync("revoked", true))
					.ReturnsAsync(token);

			// Act
			var result = await service.ValidateRefreshTokenAsync("revoked");

			// Assert
			Assert.False(result);
		}

		[Fact]
		public async Task ValidateRefreshTokenAsync_ShouldReturnFalse_WhenExpired()
		{
			// Arrange
			var service = CreateService();
			var token = new RefreshToken
			{
				RefreshTokenValue = "expired",
				ExpiresAt = DateTime.UtcNow.AddDays(-1),
				IsRevoked = false
			};
			_uowMock.Setup(x => x.RefreshTokens.GetByTokenAsync("expired", true))
					.ReturnsAsync(token);

			// Act
			var result = await service.ValidateRefreshTokenAsync("expired");

			// Assert
			Assert.False(result);
		}

		[Fact]
		public async Task RevokeTokensByUserIdAsync_ShouldNotThrow_WhenNoActiveTokens()
		{
			// Arrange
			var service = CreateService();
			_uowMock.Setup(x => x.RefreshTokens.GetByUserIdAsync(5, true))
					.ReturnsAsync(new List<RefreshToken>());

			// Act
			await service.RevokeTokensByUserIdAsync(5);

			// Assert
			_uowMock.Verify(x => x.RefreshTokens.UpdateAsync(It.IsAny<RefreshToken>()), Times.Never);
			_uowMock.Verify(x => x.SaveChangesAsync(), Times.Never);
		}

		[Fact]
		public async Task RevokeAllAsync_ShouldRevokeAllActiveTokens()
		{
			// Arrange
			var service = CreateService();
			var tokens = new List<RefreshToken>
		{
			new RefreshToken { RefreshTokenValue = "t1", IsRevoked = false },
			new RefreshToken { RefreshTokenValue = "t2", IsRevoked = false }
		};
			_uowMock.Setup(x => x.RefreshTokens.GetByUserIdAsync(10, true))
					.ReturnsAsync(tokens);

			// Act
			await service.RevokeAllAsync(10);

			// Assert
			Assert.All(tokens, t => Assert.True(t.IsRevoked));
			_uowMock.Verify(x => x.SaveChangesAsync(), Times.Once);
		}
	}
}
