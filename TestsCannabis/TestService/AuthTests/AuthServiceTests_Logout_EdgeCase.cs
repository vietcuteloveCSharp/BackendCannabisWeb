namespace TestsCannabis.TestService.AuthServiceTests;
public class AuthServiceTests_Logout_EdgeCases
{
	private AuthService CreateService(
		Mock<IUnitOfWork>? uowMock = null,
		Mock<IRefreshTokenService>? refreshMock = null,
		IOptions<JwtSettings>? jwtSettings = null)
	{
		return new AuthService(
			uowMock?.Object ?? new Mock<IUnitOfWork>().Object,
			new Mock<ITokenService>().Object,
			refreshMock?.Object ?? new Mock<IRefreshTokenService>().Object,
			new Mock<IMapper>().Object,
			new PasswordHasher<User>(),
			jwtSettings ?? TestHelper.JwtSettings
		);
	}
	[Fact]
	public async Task Logout_ShouldThrow_WhenTokenValueNull()
	{
		var service = CreateService();
		await Assert.ThrowsAsync<ArgumentNullException>(() => service.LogoutAsync(1, null!));
	}

	[Fact]
	public async Task Logout_ShouldThrow_WhenTokenValueEmpty()
	{
		var service = CreateService();
		await Assert.ThrowsAsync<ArgumentNullException>(() => service.LogoutAsync(1, ""));
	}

	[Fact]
	public async Task Logout_ShouldHandle_WhenTokenHasNullUserReference()
	{
		var token = new RefreshToken
		{
			RefreshTokenValue = "abc",
			UserId = 1,
			IsRevoked = false,
			User = null
		};

		var refreshRepo = new Mock<IRefreshTokenRepository>();
		refreshRepo.Setup(r => r.GetByTokenAsync("abc", false))
				   .ReturnsAsync(token);

		var uow = new Mock<IUnitOfWork>();
		uow.Setup(u => u.RefreshTokens).Returns(refreshRepo.Object);

		var service = CreateService(uowMock: uow);

		await service.LogoutAsync(1, "abc");

		Assert.True(token.IsRevoked);
		refreshRepo.Verify(r => r.UpdateAsync(token), Times.Once);
	}

	[Fact]
	public async Task Logout_ShouldThrow_WhenUpdateAsyncFails()
	{
		var token = new RefreshToken
		{
			RefreshTokenValue = "abc",
			UserId = 1,
			IsRevoked = false
		};

		var refreshRepo = new Mock<IRefreshTokenRepository>();
		refreshRepo.Setup(r => r.GetByTokenAsync("abc", false))
				   .ReturnsAsync(token);
		refreshRepo.Setup(r => r.UpdateAsync(It.IsAny<RefreshToken>()))
				   .ThrowsAsync(new Exception("Update fail"));

		var uow = new Mock<IUnitOfWork>();
		uow.Setup(u => u.RefreshTokens).Returns(refreshRepo.Object);

		var service = CreateService(uowMock: uow);

		await Assert.ThrowsAsync<Exception>(() => service.LogoutAsync(1, "abc"));
	}

	[Fact]
	public async Task Logout_ShouldThrow_WhenGetByTokenThrows()
	{
		var refreshRepo = new Mock<IRefreshTokenRepository>();
		refreshRepo.Setup(r => r.GetByTokenAsync("boom", false))
				   .ThrowsAsync(new Exception("DB down"));

		var uow = new Mock<IUnitOfWork>();
		uow.Setup(u => u.RefreshTokens).Returns(refreshRepo.Object);

		var service = CreateService(uowMock: uow);

		await Assert.ThrowsAsync<Exception>(() => service.LogoutAsync(1, "boom"));
	}
}
	

