namespace TestsCannabis.TestService.RefreshTokenServiceTest
{
	public class RefreshTokenServiceTests_Logic
	{
		private readonly Mock<IUnitOfWork> _uowMock;
		private readonly Mock<IMapper> _mapperMock;
		private readonly Mock<ITokenService> _tokenServiceMock;
		private readonly IOptions<JwtSettings> _jwtOptions;

		public RefreshTokenServiceTests_Logic()
		{
			_uowMock = new Mock<IUnitOfWork>();
			_mapperMock = new Mock<IMapper>();
			_tokenServiceMock = new Mock<ITokenService>();

			// ✅ Load JWT config từ appsettings.test.json
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
		[Fact]
		public async Task GenerateRefreshTokenAsync_ShouldCreateAndSaveToken()
		{
			// Arrange
			var service = CreateService();
			var userId = 100;
			_uowMock.Setup(x => x.RefreshTokens.AddAsync(It.IsAny<RefreshToken>())).Returns(Task.CompletedTask);
			_uowMock.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

			// Act
			var token = await service.GenerateRefreshTokenAsync(userId);

			// Assert
			Assert.NotNull(token.RefreshTokenValue);
			Assert.Equal(userId, token.UserId);
			Assert.False(token.IsRevoked);
			Assert.True(token.ExpiresAt > DateTime.UtcNow);
			_uowMock.Verify(x => x.RefreshTokens.AddAsync(It.IsAny<RefreshToken>()), Times.Once);
			_uowMock.Verify(x => x.SaveChangesAsync(), Times.Once);
		}

		[Fact]
		public async Task StoreTokenAsync_ShouldMapAndPersist()
		{
			// Arrange
			var service = CreateService();
			var dto = new RefreshTokenDTO
			{
				UserId = 1,
				RefreshTokenValue = "abc123",
				ExpiresAt = DateTime.UtcNow.AddDays(7)
			};
			var entity = new RefreshToken { UserId = 1, RefreshTokenValue = "abc123" };

			_mapperMock.Setup(m => m.Map<RefreshToken>(dto)).Returns(entity);
			_uowMock.Setup(x => x.RefreshTokens.AddAsync(entity)).Returns(Task.CompletedTask);
			_uowMock.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

			// Act
			await service.StoreTokenAsync(dto);

			// Assert
			_mapperMock.Verify(m => m.Map<RefreshToken>(dto), Times.Once);
			_uowMock.Verify(x => x.RefreshTokens.AddAsync(entity), Times.Once);
			_uowMock.Verify(x => x.SaveChangesAsync(), Times.Once);
		}

		[Fact]
		public async Task RevokeTokenAsync_ShouldSetIsRevokedAndSave()
		{
			// Arrange
			var service = CreateService();
			var token = new RefreshToken { RefreshTokenValue = "xyz", IsRevoked = false };
			_uowMock.Setup(x => x.RefreshTokens.GetByTokenAsync("xyz", true)).ReturnsAsync(token);

			// Act
			await service.RevokeTokenAsync("xyz");

			// Assert
			Assert.True(token.IsRevoked);
			_uowMock.Verify(x => x.RefreshTokens.UpdateAsync(token), Times.Once);
			_uowMock.Verify(x => x.SaveChangesAsync(), Times.Once);
		}

		[Fact]
		public async Task ValidateRefreshTokenAsync_ShouldReturnTrue_WhenValid()
		{
			// Arrange
			var service = CreateService();
			var token = new RefreshToken
			{
				RefreshTokenValue = "valid_token",
				ExpiresAt = DateTime.UtcNow.AddDays(1),
				IsRevoked = false
			};
			_uowMock.Setup(x => x.RefreshTokens.GetByTokenAsync("valid_token", true))
					.ReturnsAsync(token);

			// Act
			var result = await service.ValidateRefreshTokenAsync("valid_token");

			// Assert
			Assert.True(result);
		}


	}
}
