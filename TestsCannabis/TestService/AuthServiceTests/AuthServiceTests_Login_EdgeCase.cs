namespace TestsCannabis.TestService.AuthServiceTests;
public class AuthServiceTests_Login_EdgeCases
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

	// --- CASE 1: User không tồn tại ---
	[Fact]
	public async Task LoginAsync_UserNotFound_ThrowsUnauthorized()
	{
		var uowMock = new Mock<IUnitOfWork>();
		uowMock.Setup(u => u.Users.GetByUsernameAsync("missing")).ReturnsAsync((User?)null);

		var service = CreateService(uowMock);

		await Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
			service.LoginAsync(new LoginResquestDTO { Username = "missing", Password = "123" })
		);
	}

	// --- CASE 2: Sai mật khẩu ---
	[Fact]
	public async Task LoginAsync_InvalidPassword_ThrowsUnauthorized()
	{
		var user = new User { UserId = 1, Username = "john", HashPassword = "hashed", Role = new Role { RoleName = ERoleName.Admin } };

		var uowMock = new Mock<IUnitOfWork>();
		uowMock.Setup(u => u.Users.GetByUsernameAsync("john")).ReturnsAsync(user);

		var passwordHasher = new Mock<IPasswordHasher<User>>();
		passwordHasher.Setup(p => p.VerifyHashedPassword(user, "hashed", "wrong"))
			.Returns(PasswordVerificationResult.Failed);

		var service = CreateService(uowMock, passwordHasher: passwordHasher.Object);

		await Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
			service.LoginAsync(new LoginResquestDTO { Username = "john", Password = "wrong" })
		);
	}

	// --- CASE 3: User không có Role ---
	[Fact]
	public async Task LoginAsync_UserWithoutRole_ThrowsNullReference()
	{
		var user = new User { UserId = 1, Username = "john", HashPassword = "hashed" };
		var uowMock = new Mock<IUnitOfWork>();
		uowMock.Setup(u => u.Users.GetByUsernameAsync("john")).ReturnsAsync(user);

		var passwordHasher = new Mock<IPasswordHasher<User>>();
		passwordHasher.Setup(p => p.VerifyHashedPassword(user, "hashed", "123"))
			.Returns(PasswordVerificationResult.Success);

		var service = CreateService(uowMock, passwordHasher: passwordHasher.Object);

		await Assert.ThrowsAsync<NullReferenceException>(() =>
			service.LoginAsync(new LoginResquestDTO { Username = "john", Password = "123" })
		);
	}

	// --- CASE 5: RememberMe = true nhưng StoreTokenAsync ném lỗi ---
	[Fact]
	public async Task LoginAsync_RememberMeTrue_StoreTokenThrows_ExceptionBubblesUp()
	{
		var user = new User
		{
			UserId = 1,
			Username = "john",
			HashPassword = new PasswordHasher<User>().HashPassword(new User(), "123"),
			Role = new Role { RoleName = ERoleName.Admin }
		};

		var uowMock = new Mock<IUnitOfWork>();
		uowMock.Setup(u => u.Users.GetByUsernameAsync("john")).ReturnsAsync(user);

		var tokenMock = new Mock<ITokenService>();
		tokenMock.Setup(t => t.GenerateAccessToken(It.IsAny<TokenPayload>())).Returns("access-token");

		var refreshMock = new Mock<IRefreshTokenService>();
		refreshMock.Setup(r => r.GenerateRefreshTokenAsync(user.UserId))
			.ReturnsAsync(new RefreshToken { RefreshTokenValue = "rftok" });
		refreshMock.Setup(r => r.StoreTokenAsync(It.IsAny<RefreshTokenDTO>()))
			.ThrowsAsync(new InvalidOperationException("DB error"));

		var mapperMock = new Mock<IMapper>();
		mapperMock.Setup(m => m.Map<UserSummaryDTO>(user)).Returns(new UserSummaryDTO { UserId = user.UserId });

		var service = CreateService(uowMock, tokenMock, refreshMock, mapperMock);

		await Assert.ThrowsAsync<InvalidOperationException>(() =>
			service.LoginAsync(new LoginResquestDTO
			{
				Username = "john",
				Password = "123",
				RememberMe = true
			})
		);
	}
}
