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

	// === CASE 1: Token không tồn tại ===
	[Fact]
	public async Task LogoutAsync_TokenNotFound_ThrowsUnauthorized()
	{
		var uowMock = new Mock<IUnitOfWork>();
		uowMock.Setup(u => u.RefreshTokens.GetByTokenAsync("missing", false))
			   .ReturnsAsync((RefreshToken?)null);

		var service = CreateService(uowMock);

		await Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
			service.LogoutAsync(1, "missing")
		);
	}

	// === CASE 2: Token không thuộc về user ===
	[Fact]
	public async Task LogoutAsync_TokenNotBelongToUser_ThrowsUnauthorized()
	{
		var token = new RefreshToken
		{
			UserId = 999, // khác user
			RefreshTokenValue = "abc"
		};

		var uowMock = new Mock<IUnitOfWork>();
		uowMock.Setup(u => u.RefreshTokens.GetByTokenAsync("abc", false))
			   .ReturnsAsync(token);

		var service = CreateService(uowMock);

		await Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
			service.LogoutAsync(1, "abc")
		);
	}

	// === CASE 3: SaveChangesAsync bị lỗi ===
	[Fact]
	public async Task LogoutAsync_SaveChangesFails_Throws()
	{
		var token = new RefreshToken
		{
			UserId = 1,
			RefreshTokenValue = "abc"
		};

		var uowMock = new Mock<IUnitOfWork>();
		uowMock.Setup(u => u.RefreshTokens.GetByTokenAsync("abc", false))
			   .ReturnsAsync(token);
		uowMock.Setup(u => u.SaveChangesAsync()).ThrowsAsync(new Exception("DB Error"));

		var service = CreateService(uowMock);

		await Assert.ThrowsAsync<Exception>(() =>
			service.LogoutAsync(1, "abc")
		);
	}
}
