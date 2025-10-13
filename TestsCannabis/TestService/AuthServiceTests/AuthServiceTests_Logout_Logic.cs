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

	// === CASE 1: Logout thành công ===
	[Fact]
	public async Task LogoutAsync_ValidToken_MarksAsRevokedAndSaves()
	{
		// Arrange
		var userId = 10;
		var refreshTokenValue = "valid-token";

		var tokenEntity = new RefreshToken
		{
			UserId = userId,
			RefreshTokenValue = refreshTokenValue,
			IsRevoked = false
		};

		var uowMock = new Mock<IUnitOfWork>();
		uowMock.Setup(u => u.RefreshTokens.GetByTokenAsync(refreshTokenValue, false))
			   .ReturnsAsync(tokenEntity);

		var refreshMock = new Mock<IRefreshTokenService>();

		var service = CreateService(uowMock: uowMock, refreshMock: refreshMock);

		// Act
		await service.LogoutAsync(userId, refreshTokenValue);

		// Assert
		Assert.True(tokenEntity.IsRevoked);
		uowMock.Verify(u => u.RefreshTokens.UpdateAsync(tokenEntity), Times.Once);
		uowMock.Verify(u => u.SaveChangesAsync(), Times.Once);
	}
}
