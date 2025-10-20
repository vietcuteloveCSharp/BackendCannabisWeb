using DTO.DTOs.User.Users;

namespace TestsCannabis.TestService.AuthServiceTests;
public class AuthServiceTests_Login_Logic
{
	private AuthService CreateService(
	Mock<IUnitOfWork>? uowMock = null,
	Mock<ITokenService>? tokenMock = null,
	Mock<IRefreshTokenService>? refreshMock = null,
	Mock<IMapper>? mapperMock = null,
	IPasswordHasher<User>? passwordHasher = null,
	IOptions<JwtSettings>? jwtSettings =null) // bắt buộc phải có
	{
		return new AuthService(
			uowMock?.Object ?? new Mock<IUnitOfWork>().Object,
			tokenMock?.Object ?? new Mock<ITokenService>().Object,
			refreshMock?.Object ?? new Mock<IRefreshTokenService>().Object,
			mapperMock?.Object ?? new Mock<IMapper>().Object,
			passwordHasher ?? new PasswordHasher<User>(),
			jwtSettings?? TestHelper.JwtSettings
		);
	}

	[Fact]
	public async Task Login_ShouldThrow_WhenUserNotFound()
	{
		var uow = new Mock<IUnitOfWork>();
		uow.Setup(u => u.Users.GetByUsernameAsync(It.IsAny<string>())).ReturnsAsync((User?)null);

		var service = CreateService(uowMock: uow);

		var dto = new LoginResquestDTO { Username = "ghost", Password = "123" };

		await Assert.ThrowsAsync<UnauthorizedAccessException>(() => service.LoginAsync(dto));
	}

	[Fact]
	public async Task Login_ShouldThrow_WhenPasswordInvalid()
	{
		var user = new User { Username = "alice", HashPassword = "hashed" };

		var uow = new Mock<IUnitOfWork>();
		uow.Setup(u => u.Users.GetByUsernameAsync("alice")).ReturnsAsync(user);

		var passwordHasher = new Mock<IPasswordHasher<User>>();
		passwordHasher
			.Setup(p => p.VerifyHashedPassword(user, user.HashPassword, "wrong"))
			.Returns(PasswordVerificationResult.Failed);

		var service = CreateService(uowMock: uow, passwordHasher: passwordHasher.Object);

		var dto = new LoginResquestDTO { Username = "alice", Password = "wrong" };

		await Assert.ThrowsAsync<UnauthorizedAccessException>(() => service.LoginAsync(dto));
	}

	[Fact]
	public async Task Login_ShouldReturnToken_WhenRememberMeFalse()
	{
		var user = new User
		{
			UserId = 1,
			Username = "bob",
			HashPassword = "hashed",
			Role = new Role { RoleName = ERoleName.Admin }
		};

		var uow = new Mock<IUnitOfWork>();
		uow.Setup(u => u.Users.GetByUsernameAsync("bob")).ReturnsAsync(user);

		var tokenService = new Mock<ITokenService>();
		tokenService.Setup(t => t.GenerateAccessToken(It.IsAny<TokenPayload>())).Returns("access-token");

		var mapper = new Mock<IMapper>();
		mapper.Setup(m => m.Map<UserSummaryDTO>(user)).Returns(new UserSummaryDTO { UserId = 1, Username = "bob", RoleName = "Admin" });

		var passwordHasher = new Mock<IPasswordHasher<User>>();
		passwordHasher
			.Setup(p => p.VerifyHashedPassword(user, user.HashPassword, "123"))
			.Returns(PasswordVerificationResult.Success);

		var service = CreateService(uow, tokenService, mapperMock: mapper, passwordHasher: passwordHasher.Object);

		var dto = new LoginResquestDTO { Username = "bob", Password = "123", RememberMe = false };

		var result = await service.LoginAsync(dto);

		Assert.Equal("access-token", result.AccessToken);
		Assert.Null(result.RefreshToken);
		Assert.NotNull(result.User);
	}

	[Fact]
	public async Task Login_ShouldReturnTokenWithRefresh_WhenRememberMeTrue()
	{
		var user = new User
		{
			UserId = 99,
			Username = "john",
			HashPassword = "hashed",
			Role = new Role { RoleName =ERoleName.Admin}
		};

		var uow = new Mock<IUnitOfWork>();
		uow.Setup(u => u.Users.GetByUsernameAsync("john")).ReturnsAsync(user);

		var tokenService = new Mock<ITokenService>();
		tokenService.Setup(t => t.GenerateAccessToken(It.IsAny<TokenPayload>())).Returns("jwt-token");

		var refreshMock = new Mock<IRefreshTokenService>();
		refreshMock.Setup(r => r.GenerateRefreshTokenAsync(user.UserId))
			.ReturnsAsync(new RefreshToken { RefreshTokenValue = "refresh123", UserId = user.UserId });
		refreshMock.Setup(r => r.StoreTokenAsync(It.IsAny<RefreshTokenDTO>())).Returns(Task.CompletedTask);

		var passwordHasher = new Mock<IPasswordHasher<User>>();
		passwordHasher
			.Setup(p => p.VerifyHashedPassword(user, user.HashPassword, "123"))
			.Returns(PasswordVerificationResult.Success);

		var mapper = new Mock<IMapper>();
		mapper.Setup(m => m.Map<UserSummaryDTO>(user)).Returns(new UserSummaryDTO { UserId = 99, Username = "john", RoleName = "Admin" });

		var service = CreateService(uow, tokenService, refreshMock, mapper, passwordHasher.Object);

		var dto = new LoginResquestDTO { Username = "john", Password = "123", RememberMe = true };

		var result = await service.LoginAsync(dto);

		Assert.Equal("jwt-token", result.AccessToken);
		Assert.Equal("refresh123", result.RefreshToken);
		refreshMock.Verify(r => r.GenerateRefreshTokenAsync(user.UserId), Times.Once);
		refreshMock.Verify(r => r.StoreTokenAsync(It.IsAny<RefreshTokenDTO>()), Times.Once);
	}

	[Fact]
	public async Task Login_ShouldThrow_WhenRoleIsNull()
	{
		var user = new User { UserId = 1, Username = "alice", HashPassword = "hashed", Role = null };

		var uow = new Mock<IUnitOfWork>();
		uow.Setup(u => u.Users.GetByUsernameAsync("alice")).ReturnsAsync(user);

		var passwordHasher = new Mock<IPasswordHasher<User>>();
		passwordHasher
			.Setup(p => p.VerifyHashedPassword(user, user.HashPassword, "123"))
			.Returns(PasswordVerificationResult.Success);

		var service = CreateService(uow, passwordHasher: passwordHasher.Object);

		var dto = new LoginResquestDTO { Username = "alice", Password = "123" };

		await Assert.ThrowsAsync<NullReferenceException>(() => service.LoginAsync(dto));
	}
}

	