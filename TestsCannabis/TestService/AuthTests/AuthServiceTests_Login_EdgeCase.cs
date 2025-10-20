using DTO.DTOs.User.Users;

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
	[Fact]
	public async Task Login_ShouldThrow_WhenDtoNull()
	{
		var service = CreateService();
		await Assert.ThrowsAsync<NullReferenceException>(() => service.LoginAsync(null!));
	}

	[Theory]
	[InlineData("")]
	[InlineData("   ")]
	public async Task Login_ShouldThrow_WhenUsernameEmpty(string username)
	{
		var uow = new Mock<IUnitOfWork>();
		uow.Setup(u => u.Users.GetByUsernameAsync(username)).ReturnsAsync((User?)null);

		var service = CreateService(uow);
		var dto = new LoginResquestDTO { Username = username, Password = "123" };

		await Assert.ThrowsAsync<UnauthorizedAccessException>(() => service.LoginAsync(dto));
	}

	[Fact]
	public async Task Login_ShouldThrow_WhenPasswordEmpty()
	{
		var user = new User { Username = "a", HashPassword = "h", Role = new Role { RoleName = ERoleName.Admin } };
		var uow = new Mock<IUnitOfWork>();
		uow.Setup(u => u.Users.GetByUsernameAsync("a")).ReturnsAsync(user);

		var passwordHasher = new Mock<IPasswordHasher<User>>();
		passwordHasher
			.Setup(p => p.VerifyHashedPassword(user, user.HashPassword, It.IsAny<string>()))
			.Returns(PasswordVerificationResult.Failed);

		var service = CreateService(uow, passwordHasher: passwordHasher.Object);
		var dto = new LoginResquestDTO { Username = "a", Password = "" };

		await Assert.ThrowsAsync<UnauthorizedAccessException>(() => service.LoginAsync(dto));
	}

	[Fact]
	public async Task Login_ShouldHandle_WhenJwtLifetimeIsZero()
	{
		var jwtSettings = Options.Create(new JwtSettings
		{
			Key = "key",
			Issuer = "issuer",
			Audience = "aud",
			AccessTokenLifetimeMinutes = 0,
			RefreshTokenLifetimeDays = 1
		});

		var user = new User
		{
			UserId = 1,
			Username = "a",
			HashPassword = "h",
			Role = new Role { RoleName = ERoleName.Employee }
		};

		var uow = new Mock<IUnitOfWork>();
		uow.Setup(u => u.Users.GetByUsernameAsync("a")).ReturnsAsync(user);

		var tokenService = new Mock<ITokenService>();
		tokenService.Setup(t => t.GenerateAccessToken(It.IsAny<TokenPayload>())).Returns("jwt");

		var passwordHasher = new Mock<IPasswordHasher<User>>();
		passwordHasher
			.Setup(p => p.VerifyHashedPassword(user, user.HashPassword, "123"))
			.Returns(PasswordVerificationResult.Success);

		var mapper = new Mock<IMapper>();
		mapper.Setup(m => m.Map<UserSummaryDTO>(user))
			.Returns(new UserSummaryDTO { Username = "a", RoleName = "Customer" });

		var service = CreateService(
			uow, tokenMock: tokenService, mapperMock: mapper,
			passwordHasher: passwordHasher.Object, jwtSettings: jwtSettings);

		var dto = new LoginResquestDTO { Username = "a", Password = "123" };
		var result = await service.LoginAsync(dto);

		Assert.True(result.Expiration <= DateTime.UtcNow.AddSeconds(1));
	}

	[Fact]
	public async Task Login_ShouldThrow_WhenUserRoleIsNull()
	{
		var user = new User { UserId = 1, Username = "a", HashPassword = "h", Role = null };

		var uow = new Mock<IUnitOfWork>();
		uow.Setup(u => u.Users.GetByUsernameAsync("a")).ReturnsAsync(user);

		var passwordHasher = new Mock<IPasswordHasher<User>>();
		passwordHasher
			.Setup(p => p.VerifyHashedPassword(user, user.HashPassword, "123"))
			.Returns(PasswordVerificationResult.Success);

		var service = CreateService(uow, passwordHasher: passwordHasher.Object);

		await Assert.ThrowsAsync<NullReferenceException>(() =>
			service.LoginAsync(new LoginResquestDTO { Username = "a", Password = "123" }));
	}

	// =========================
	// ✅ EDGE CASE 6–10 (bổ sung)
	// =========================

	[Fact]
	public async Task Login_ShouldThrow_WhenUserHasNoHashPassword()
	{
		var user = new User { Username = "a", HashPassword = null, Role = new Role { RoleName = ERoleName.Admin } };
		var uow = new Mock<IUnitOfWork>();
		uow.Setup(u => u.Users.GetByUsernameAsync("a")).ReturnsAsync(user);

		var service = CreateService(uow);
		var dto = new LoginResquestDTO { Username = "a", Password = "123" };

		await Assert.ThrowsAsync<ArgumentNullException>(() => service.LoginAsync(dto));
	}

	[Fact]
	public async Task Login_ShouldReturnNullAccessToken_WhenTokenServiceReturnsNull()
	{
		var user = new User { UserId = 1, Username = "a", HashPassword = "h", Role = new Role { RoleName = ERoleName.Admin } };
		var uow = new Mock<IUnitOfWork>();
		uow.Setup(u => u.Users.GetByUsernameAsync("a")).ReturnsAsync(user);

		var tokenService = new Mock<ITokenService>();
		tokenService.Setup(t => t.GenerateAccessToken(It.IsAny<TokenPayload>())).Returns((string)null);

		var passwordHasher = new Mock<IPasswordHasher<User>>();
		passwordHasher.Setup(p => p.VerifyHashedPassword(user, user.HashPassword, "123"))
			.Returns(PasswordVerificationResult.Success);

		var service = CreateService(uow, tokenMock: tokenService, passwordHasher: passwordHasher.Object);
		var result = await service.LoginAsync(new LoginResquestDTO { Username = "a", Password = "123" });

		Assert.Null(result.AccessToken);
	}

	[Fact]
	public async Task Login_ShouldThrow_WhenRefreshTokenIsNull()
	{
		var user = new User { UserId = 1, Username = "a", HashPassword = "h", Role = new Role { RoleName = ERoleName.Admin } };
		var uow = new Mock<IUnitOfWork>();
		uow.Setup(u => u.Users.GetByUsernameAsync("a")).ReturnsAsync(user);

		var refreshMock = new Mock<IRefreshTokenService>();
		refreshMock.Setup(r => r.GenerateRefreshTokenAsync(user.UserId)).ReturnsAsync((RefreshToken?)null);

		var passwordHasher = new Mock<IPasswordHasher<User>>();
		passwordHasher.Setup(p => p.VerifyHashedPassword(user, user.HashPassword, "123"))
			.Returns(PasswordVerificationResult.Success);

		var service = CreateService(uow, refreshMock: refreshMock, passwordHasher: passwordHasher.Object);

		await Assert.ThrowsAsync<NullReferenceException>(() =>
			service.LoginAsync(new LoginResquestDTO { Username = "a", Password = "123", RememberMe = true }));
	}

	[Fact]
	public async Task Login_ShouldReturn_WhenMapperReturnsNull()
	{
		var user = new User { UserId = 1, Username = "a", HashPassword = "h", Role = new Role { RoleName = ERoleName.Admin } };
		var uow = new Mock<IUnitOfWork>();
		uow.Setup(u => u.Users.GetByUsernameAsync("a")).ReturnsAsync(user);

		var mapper = new Mock<IMapper>();
		mapper.Setup(m => m.Map<UserSummaryDTO>(user)).Returns((UserSummaryDTO?)null);

		var tokenService = new Mock<ITokenService>();
		tokenService.Setup(t => t.GenerateAccessToken(It.IsAny<TokenPayload>())).Returns("token");

		var passwordHasher = new Mock<IPasswordHasher<User>>();
		passwordHasher.Setup(p => p.VerifyHashedPassword(user, "h", "123"))
			.Returns(PasswordVerificationResult.Success);

		var service = CreateService(uow, tokenMock: tokenService, mapperMock: mapper, passwordHasher: passwordHasher.Object);
		var result = await service.LoginAsync(new LoginResquestDTO { Username = "a", Password = "123" });

		Assert.Null(result.User);
		Assert.NotNull(result.AccessToken);
	}

	[Fact]
	public async Task Login_ShouldThrow_WhenUserRepoThrows()
	{
		var uow = new Mock<IUnitOfWork>();
		uow.Setup(u => u.Users.GetByUsernameAsync("a")).ThrowsAsync(new Exception("DB down"));

		var service = CreateService(uow);
		await Assert.ThrowsAsync<Exception>(() =>
			service.LoginAsync(new LoginResquestDTO { Username = "a", Password = "123" }));
	}
}

	

