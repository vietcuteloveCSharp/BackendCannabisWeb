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
	// === 1. Login thành công, RememberMe = false ===
	[Fact]
	public async Task LoginAsync_ValidCredentials_RememberMeFalse_ReturnsAccessTokenOnly()
	{
		var user = new User
		{
			UserId = 1,
			Username = "john",
			Role = new Role { RoleName = ERoleName.Admin }
		};
		user.HashPassword = new PasswordHasher<User>().HashPassword(user, "123");

		var uowMock = new Mock<IUnitOfWork>();
		uowMock.Setup(u => u.Users.GetByUsernameAsync("john")).ReturnsAsync(user);

		var tokenMock = new Mock<ITokenService>();
		tokenMock.Setup(t => t.GenerateAccessToken(It.IsAny<TokenPayload>())).Returns("access-token");

		var mapperMock = new Mock<IMapper>();
		mapperMock.Setup(m => m.Map<UserSummaryDTO>(user)).Returns(new UserSummaryDTO { UserId = user.UserId });

		var service = CreateService(uowMock,tokenMock,null,mapperMock,new PasswordHasher<User>(), TestHelper.JwtSettings); // lấy từ config

		var result = await service.LoginAsync(new LoginResquestDTO
		{
			Username = "john",
			Password = "123",
			RememberMe = false
		});

		Assert.Equal("access-token", result.AccessToken);
		Assert.Null(result.RefreshToken);
	}

	// === 2. Login thành công, RememberMe = true ===
	[Fact]
	public async Task LoginAsync_ValidCredentials_RememberMeTrue_ReturnsAccessAndRefreshToken()
	{
		var user = new User
		{
			UserId = 1,
			Username = "john",
			Role = new Role { RoleName = ERoleName.Admin }
		};
		var refreshTokenEntity = new RefreshToken
		{
			RefreshTokenValue = "refresh-token",
			UserId = user.UserId,
			ExpiresAt = DateTime.UtcNow.AddDays(7)
		};
		user.HashPassword = new PasswordHasher<User>().HashPassword(user, "123");

		var uowMock = new Mock<IUnitOfWork>();
		uowMock.Setup(u => u.Users.GetByUsernameAsync("john")).ReturnsAsync(user);

		var tokenMock = new Mock<ITokenService>();
		tokenMock.Setup(t => t.GenerateAccessToken(It.IsAny<TokenPayload>())).Returns("access-token");

		var refreshMock = new Mock<IRefreshTokenService>();
		refreshMock.Setup(r => r.GenerateRefreshTokenAsync(user.UserId)).ReturnsAsync(refreshTokenEntity);

		var mapperMock = new Mock<IMapper>();
		mapperMock.Setup(m => m.Map<UserSummaryDTO>(user)).Returns(new UserSummaryDTO { UserId = user.UserId });

		var service = CreateService(uowMock,tokenMock,refreshMock,mapperMock,null,null);

		var result = await service.LoginAsync(new LoginResquestDTO
		{
			Username = "john",
			Password = "123",
			RememberMe = true
		});

		Assert.Equal("access-token", result.AccessToken);
		Assert.Equal("refresh-token", result.RefreshToken);
	}

	// === 3. Kiểm tra role payload ===
	[Fact]
	public async Task LoginAsync_UserWithRoleUser_ReturnsPayloadWithRoleUser()
	{
		var user = new User
		{
			UserId = 1,
			Username = "john",
			Role = new Role { RoleName = ERoleName.User }
		};
		user.HashPassword = new PasswordHasher<User>().HashPassword(user, "123");

		var uowMock = new Mock<IUnitOfWork>();
		uowMock.Setup(u => u.Users.GetByUsernameAsync("john")).ReturnsAsync(user);

		TokenPayload? capturedPayload = null;
		var tokenMock = new Mock<ITokenService>();
		tokenMock.Setup(t => t.GenerateAccessToken(It.IsAny<TokenPayload>()))
				 .Callback<TokenPayload>(p => capturedPayload = p)
				 .Returns("access-token");

		var mapperMock = new Mock<IMapper>();
		mapperMock.Setup(m => m.Map<UserSummaryDTO>(user)).Returns(new UserSummaryDTO { UserId = user.UserId });

		var service = CreateService(uowMock,tokenMock,null,mapperMock,null,null);

		var result = await service.LoginAsync(new LoginResquestDTO { Username = "john", Password = "123" });

		Assert.Equal("access-token", result.AccessToken);
		Assert.Equal(ERoleName.User.ToString(), capturedPayload?.Role);
	}

	[Fact]
	public async Task LoginAsync_UserWithRoleAdmin_ReturnsPayloadWithRoleAdmin()
	{
		var user = new User
		{
			UserId = 1,
			Username = "john",
			Role = new Role { RoleName = ERoleName.Admin }
		};
		user.HashPassword = new PasswordHasher<User>().HashPassword(user, "123");

		var uowMock = new Mock<IUnitOfWork>();
		uowMock.Setup(u => u.Users.GetByUsernameAsync("john")).ReturnsAsync(user);

		TokenPayload? capturedPayload = null;
		var tokenMock = new Mock<ITokenService>();
		tokenMock.Setup(t => t.GenerateAccessToken(It.IsAny<TokenPayload>()))
				 .Callback<TokenPayload>(p => capturedPayload = p)
				 .Returns("access-token");

		var mapperMock = new Mock<IMapper>();
		mapperMock.Setup(m => m.Map<UserSummaryDTO>(user)).Returns(new UserSummaryDTO { UserId = user.UserId });

		var service = CreateService(uowMock,tokenMock,null,mapperMock,null,null);

		var result = await service.LoginAsync(new LoginResquestDTO { Username = "john", Password = "123" });

		Assert.Equal("access-token", result.AccessToken);
		Assert.Equal(ERoleName.Admin.ToString(), capturedPayload?.Role);
	}

	// === 4. Kiểm tra expiration token ===
	[Fact]
	public async Task LoginAsync_AccessTokenLifetimeZero_ReturnsExpiredToken()
	{
		var user = new User { UserId = 1, Username = "john", Role = new Role { RoleName = ERoleName.Admin } };
		user.HashPassword = new PasswordHasher<User>().HashPassword(user, "123");

		var uowMock = new Mock<IUnitOfWork>();
		uowMock.Setup(u => u.Users.GetByUsernameAsync("john")).ReturnsAsync(user);

		var tokenMock = new Mock<ITokenService>();
		tokenMock.Setup(t => t.GenerateAccessToken(It.IsAny<TokenPayload>())).Returns("access-token");

		// override JWT lifetime bằng 0 phút
		var jwtSettings = Options.Create(new JwtSettings
		{
			Key = TestHelper.JwtSettings.Value.Key,
			AccessTokenLifetimeMinutes = 0,
			RefreshTokenLifetimeDays = 7,
			Issuer = TestHelper.JwtSettings.Value.Issuer,
			Audience = TestHelper.JwtSettings.Value.Audience
		});

		var service = CreateService(uowMock,tokenMock,null,null,null,jwtSettings);

		var result = await service.LoginAsync(new LoginResquestDTO { Username = "john", Password = "123" });

		Assert.True(result.Expiration <= DateTime.UtcNow);
	}

}