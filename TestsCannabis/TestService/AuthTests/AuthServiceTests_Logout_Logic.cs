namespace TestsCannabis.TestService.AuthServiceTests;
public class AuthServiceTests_Logout_Logic
{
	private AuthService CreateService(
		Mock<IUnitOfWork>? uowMock = null,
		Mock<ITokenService>? tokenMock = null,
		Mock<IRefreshTokenService>? refreshMock = null,
		Mock<IMapper>? mapperMock = null,
		IPasswordHasher<User>? passwordHasher = null,
		IOptions<JwtSettings>? jwtSettings = null)
	{
		return new AuthService(
			uowMock?.Object ?? new Mock<IUnitOfWork>().Object,
			tokenMock?.Object ?? new Mock<ITokenService>().Object,
			refreshMock?.Object ?? new Mock<IRefreshTokenService>().Object,
			mapperMock?.Object ?? new Mock<IMapper>().Object,
			passwordHasher ?? new PasswordHasher<User>(),
			jwtSettings ?? TestHelper.JwtSettings
		);
	}
	[Fact]
	public async Task Logout_ShouldRevokeToken_WhenValid()
	{
		// Arrange
		var token = new RefreshToken
		{
			RefreshTokenValue = "valid",
			UserId = 1,
			IsRevoked = false
		};

		var refreshRepo = new Mock<IRefreshTokenRepository>();
		refreshRepo.Setup(r => r.GetByTokenAsync("valid", false))
				   .ReturnsAsync(token);

		var uow = new Mock<IUnitOfWork>();
		uow.Setup(u => u.RefreshTokens).Returns(refreshRepo.Object);

		var service = CreateService(uowMock: uow);

		// Act
		await service.LogoutAsync(1, "valid");

		// Assert
		Assert.True(token.IsRevoked);
		refreshRepo.Verify(r => r.UpdateAsync(token), Times.Once);
		uow.Verify(u => u.SaveChangesAsync(), Times.Once);
	}

	[Fact]
	public async Task Logout_ShouldThrow_WhenTokenNotFound()
	{
		var refreshRepo = new Mock<IRefreshTokenRepository>();
		refreshRepo.Setup(r => r.GetByTokenAsync("missing", false))
				   .ReturnsAsync((RefreshToken?)null);

		var uow = new Mock<IUnitOfWork>();
		uow.Setup(u => u.RefreshTokens).Returns(refreshRepo.Object);

		var service = CreateService(uowMock: uow);

		await Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
			service.LogoutAsync(1, "missing"));
	}

	[Fact]
	public async Task Logout_ShouldThrow_WhenTokenBelongsToAnotherUser()
	{
		var token = new RefreshToken
		{
			RefreshTokenValue = "alien",
			UserId = 99,
			IsRevoked = false
		};

		var refreshRepo = new Mock<IRefreshTokenRepository>();
		refreshRepo.Setup(r => r.GetByTokenAsync("alien", false))
				   .ReturnsAsync(token);

		var uow = new Mock<IUnitOfWork>();
		uow.Setup(u => u.RefreshTokens).Returns(refreshRepo.Object);

		var service = CreateService(uowMock: uow);

		await Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
			service.LogoutAsync(1, "alien"));
	}

	[Fact]
	public async Task Logout_ShouldDoNothing_WhenTokenAlreadyRevoked()
	{
		var token = new RefreshToken
		{
			RefreshTokenValue = "revoked",
			UserId = 1,
			IsRevoked = true
		};

		var refreshRepo = new Mock<IRefreshTokenRepository>();
		refreshRepo.Setup(r => r.GetByTokenAsync("revoked", false))
				   .ReturnsAsync(token);

		var uow = new Mock<IUnitOfWork>();
		uow.Setup(u => u.RefreshTokens).Returns(refreshRepo.Object);

		var service = CreateService(uowMock: uow);

		await service.LogoutAsync(1, "revoked");

		refreshRepo.Verify(r => r.UpdateAsync(It.IsAny<RefreshToken>()), Times.Never);
		uow.Verify(u => u.SaveChangesAsync(), Times.Never);
	}

	[Fact]
	public async Task Logout_ShouldThrow_WhenSaveChangesFails()
	{
		var token = new RefreshToken
		{
			RefreshTokenValue = "valid",
			UserId = 1,
			IsRevoked = false
		};

		var refreshRepo = new Mock<IRefreshTokenRepository>();
		refreshRepo.Setup(r => r.GetByTokenAsync("valid", false))
				   .ReturnsAsync(token);

		var uow = new Mock<IUnitOfWork>();
		uow.Setup(u => u.RefreshTokens).Returns(refreshRepo.Object);
		uow.Setup(u => u.SaveChangesAsync()).ThrowsAsync(new Exception("DB error"));

		var service = CreateService(uowMock: uow);

		await Assert.ThrowsAsync<Exception>(() =>
			service.LogoutAsync(1, "valid"));
	}
}



