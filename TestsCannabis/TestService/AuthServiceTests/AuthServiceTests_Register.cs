using Microsoft.AspNetCore.Identity;

namespace TestsCannabis.TestService.AuthServiceTests
{
	public class AuthServiceTests_Register
	{
		private readonly JwtSettings _jwtSettings = new JwtSettings
		{
			Key = "w7vd2kaUnU7mLCqK9vKfVwIDPUvT4KiYzL58JfKtqRmmrFb7bG3kG8zLBvKq3A6ZHg==",
			AccessTokenLifetimeMinutes = 30,
			RefreshTokenLifetimeDays = 7,
			Issuer = "https://localhost:7206",
			Audience = "https://localhost:4000"
		};
		private AuthService CreateService(
			Mock<IUnitOfWork>? uowMock = null,
			Mock<IMapper>? mapperMock = null
			)
		{
			return new AuthService(
			uowMock?.Object ?? new Mock<IUnitOfWork>().Object,
			new Mock<ITokenService>().Object,
			new Mock<IRefreshTokenService>().Object,
			mapperMock?.Object ?? new Mock<IMapper>().Object,
			new Mock<IPasswordHasher<User>>().Object,           // đúng thứ tự bây giờ
			 Options.Create(new JwtSettings()));
		}
		// === 1. Happy path ===
		[Fact]
		public async Task RegisterUserAsync_Success_ReturnsUserDTO()
		{
			var createDto = new CreateUserDTO { Email = "test@test.com", Username = "john", Password = "123" };
			var userEntity = new User { UserId = 1, Email = createDto.Email, Username = createDto.Username };
			var savedEntity = new User { UserId = 1, Email = createDto.Email, Username = createDto.Username, HashPassword = "hashed" };
			var userDto = new UserDTO { UserId = 1, Email = createDto.Email, Username = createDto.Username };

			var uowMock = new Mock<IUnitOfWork>();
			uowMock.Setup(u => u.Users.EmailExistsAsync(createDto.Email)).ReturnsAsync(false);
			uowMock.Setup(u => u.Users.UserNameExistsAsync(createDto.Username)).ReturnsAsync(false);
			uowMock.Setup(u => u.Users.AddAsync(It.IsAny<User>())).ReturnsAsync(savedEntity);

			var mapperMock = new Mock<IMapper>();
			mapperMock.Setup(m => m.Map<User>(createDto)).Returns(userEntity);
			mapperMock.Setup(m => m.Map<UserDTO>(savedEntity)).Returns(userDto);

			var service = CreateService(uowMock, mapperMock);

			var result = await service.RegisterUserAsync(createDto);

			Assert.NotNull(result);
			Assert.Equal(userDto.UserId, result.UserId);
			Assert.NotEqual(createDto.Password, savedEntity.HashPassword);
			uowMock.Verify(u => u.SaveChangesAsync(), Times.Once);
		}
		// === 2. Negative input ===
		[Fact]
		public async Task RegisterUserAsync_NullInput_ThrowsArgumentNull()
		{
			var service = CreateService();
			await Assert.ThrowsAsync<ArgumentNullException>(() => service.RegisterUserAsync(null!));
		}

		[Fact]
		public async Task RegisterUserAsync_EmailExists_ThrowsInvalidOperation()
		{
			var dto = new CreateUserDTO { Email = "test@test.com", Username = "john", Password = "123" };
			var uowMock = new Mock<IUnitOfWork>();
			uowMock.Setup(u => u.Users.EmailExistsAsync(dto.Email)).ReturnsAsync(true);

			var service = CreateService(uowMock);

			await Assert.ThrowsAsync<InvalidOperationException>(() => service.RegisterUserAsync(dto));
		}
		[Fact]
		public async Task RegisterUserAsync_UserNameExists_ThrowsInvalidOperation()
		{
			var dto = new CreateUserDTO { Email = "test@test.com", Username = "john", Password = "123" };
			var uowMock = new Mock<IUnitOfWork>();
			uowMock.Setup(u => u.Users.EmailExistsAsync(dto.Email)).ReturnsAsync(false);
			uowMock.Setup(u => u.Users.UserNameExistsAsync(dto.Username)).ReturnsAsync(true);

			var service = CreateService(uowMock);

			await Assert.ThrowsAsync<InvalidOperationException>(() => service.RegisterUserAsync(dto));
		}
		[Fact]
		public async Task RegisterUserAsync_PasswordNull_ThrowsArgumentNull()
		{
			var dto = new CreateUserDTO { Email = "test@test.com", Username = "john", Password = null! };
			var uowMock = new Mock<IUnitOfWork>();
			uowMock.Setup(u => u.Users.EmailExistsAsync(dto.Email)).ReturnsAsync(false);
			uowMock.Setup(u => u.Users.UserNameExistsAsync(dto.Username)).ReturnsAsync(false);

			var mapperMock = new Mock<IMapper>();
			mapperMock.Setup(m => m.Map<User>(dto)).Returns(new User { Username = dto.Username, Email = dto.Email });

			var service = CreateService(uowMock, mapperMock);

			await Assert.ThrowsAsync<ArgumentNullException>(() => service.RegisterUserAsync(dto));
		}
		[Fact]
		public async Task RegisterUserAsync_PasswordEmpty_StillHashes()
		{
			// Arrange
			var dto = new CreateUserDTO { Email = "test@test.com", Username = "john", Password = "" };
			var entity = new User { UserId = 1, Email = dto.Email, Username = dto.Username };

			var uowMock = new Mock<IUnitOfWork>();
			uowMock.Setup(u => u.Users.EmailExistsAsync(dto.Email)).ReturnsAsync(false);
			uowMock.Setup(u => u.Users.UserNameExistsAsync(dto.Username)).ReturnsAsync(false);
			uowMock.Setup(u => u.Users.AddAsync(It.IsAny<User>())).ReturnsAsync((User u) => u);

			var mapperMock = new Mock<IMapper>();
			mapperMock.Setup(m => m.Map<User>(dto)).Returns((CreateUserDTO d) => new User { Email = d.Email, Username = d.Username });
			mapperMock.Setup(m => m.Map<UserDTO>(It.IsAny<User>()))
					  .Returns((User u) => new UserDTO { UserId = u.UserId, Email = u.Email!, Username = u.Username! });

			var service = CreateService(uowMock, mapperMock);

			// Act
			var result = await service.RegisterUserAsync(dto);

			// Assert
			Assert.NotNull(result);
			Assert.False(string.IsNullOrEmpty(result.UserId.ToString())); // kiểm tra UserId tồn tại
			Assert.False(string.IsNullOrEmpty(result.Email));
		}
		[Fact]
		public async Task RegisterUserAsync_EmailNull_Throws()
		{
			var dto = new CreateUserDTO { Email = null!, Username = "john", Password = "123" };
			var service = CreateService();
			await Assert.ThrowsAnyAsync<Exception>(() => service.RegisterUserAsync(dto));
		}
		[Fact]
		public async Task RegisterUserAsync_UsernameNull_Throws()
		{
			var dto = new CreateUserDTO { Email = "test@test.com", Username = null!, Password = "123" };
			var service = CreateService();
			await Assert.ThrowsAnyAsync<Exception>(() => service.RegisterUserAsync(dto));
		}
		// === 3. Mapper edge ===
		[Fact]
		public async Task RegisterUserAsync_MapperReturnsNullUser_Throws()
		{
			var dto = new CreateUserDTO { Email = "test@test.com", Username = "john", Password = "123" };
			var uowMock = new Mock<IUnitOfWork>();
			uowMock.Setup(u => u.Users.EmailExistsAsync(dto.Email)).ReturnsAsync(false);
			uowMock.Setup(u => u.Users.UserNameExistsAsync(dto.Username)).ReturnsAsync(false);

			var mapperMock = new Mock<IMapper>();
			mapperMock.Setup(m => m.Map<User>(dto)).Returns((User)null!);

			var service = CreateService(uowMock, mapperMock);

			await Assert.ThrowsAsync<NullReferenceException>(() => service.RegisterUserAsync(dto));
		}

		[Fact]
		public async Task RegisterUserAsync_MapperReturnsNullUserDto_ResultIsNull()
		{
			var dto = new CreateUserDTO { Email = "test@test.com", Username = "john", Password = "123" };
			var entity = new User { UserId = 1, Email = dto.Email, Username = dto.Username };

			var uowMock = new Mock<IUnitOfWork>();
			uowMock.Setup(u => u.Users.EmailExistsAsync(dto.Email)).ReturnsAsync(false);
			uowMock.Setup(u => u.Users.UserNameExistsAsync(dto.Username)).ReturnsAsync(false);
			uowMock.Setup(u => u.Users.AddAsync(It.IsAny<User>())).ReturnsAsync(entity);

			var mapperMock = new Mock<IMapper>();
			mapperMock.Setup(m => m.Map<User>(dto)).Returns(entity);
			mapperMock.Setup(m => m.Map<UserDTO>(entity)).Returns((UserDTO)null!);

			var service = CreateService(uowMock, mapperMock);

			var result = await service.RegisterUserAsync(dto);

			Assert.Null(result);
		}

		// === 4. Dependency failure ===
		[Fact]
		public async Task RegisterUserAsync_AddAsyncThrows_PropagatesException()
		{
			var dto = new CreateUserDTO { Email = "test@test.com", Username = "john", Password = "123" };
			var entity = new User { Email = dto.Email, Username = dto.Username };

			var uowMock = new Mock<IUnitOfWork>();
			uowMock.Setup(u => u.Users.EmailExistsAsync(dto.Email)).ReturnsAsync(false);
			uowMock.Setup(u => u.Users.UserNameExistsAsync(dto.Username)).ReturnsAsync(false);
			uowMock.Setup(u => u.Users.AddAsync(It.IsAny<User>())).ThrowsAsync(new Exception("DB error"));

			var mapperMock = new Mock<IMapper>();
			mapperMock.Setup(m => m.Map<User>(dto)).Returns(entity);

			var service = CreateService(uowMock, mapperMock);

			await Assert.ThrowsAsync<Exception>(() => service.RegisterUserAsync(dto));
		}

		[Fact]
		public async Task RegisterUserAsync_SaveChangesThrows_PropagatesException()
		{
			var dto = new CreateUserDTO { Email = "test@test.com", Username = "john", Password = "123" };
			var entity = new User { UserId = 1, Email = dto.Email, Username = dto.Username };

			var uowMock = new Mock<IUnitOfWork>();
			uowMock.Setup(u => u.Users.EmailExistsAsync(dto.Email)).ReturnsAsync(false);
			uowMock.Setup(u => u.Users.UserNameExistsAsync(dto.Username)).ReturnsAsync(false);
			uowMock.Setup(u => u.Users.AddAsync(It.IsAny<User>())).ReturnsAsync(entity);
			uowMock.Setup(u => u.SaveChangesAsync()).ThrowsAsync(new Exception("DB error"));

			var mapperMock = new Mock<IMapper>();
			mapperMock.Setup(m => m.Map<User>(dto)).Returns(entity);

			var service = CreateService(uowMock, mapperMock);

			await Assert.ThrowsAsync<Exception>(() => service.RegisterUserAsync(dto));
		}
		//Fuzzy Test Idea
		[Fact]
		public async Task RegisterUserAsync_EmailNotDetectedButDbConstraintFails_ThrowsDbUpdateException()
		{
			var dto = new CreateUserDTO { Email = "duplicate@test.com", Username = "john", Password = "123" };
			var entity = new User { Email = dto.Email, Username = dto.Username };

			var uowMock = new Mock<IUnitOfWork>();
			uowMock.Setup(u => u.Users.EmailExistsAsync(dto.Email)).ReturnsAsync(false); // giả lập repo không detect
			uowMock.Setup(u => u.Users.UserNameExistsAsync(dto.Username)).ReturnsAsync(false);
			uowMock.Setup(u => u.Users.AddAsync(It.IsAny<User>())).ReturnsAsync(entity);
			uowMock.Setup(u => u.SaveChangesAsync()).ThrowsAsync(new Microsoft.EntityFrameworkCore.DbUpdateException("Unique constraint failed"));

			var mapperMock = new Mock<IMapper>();
			mapperMock.Setup(m => m.Map<User>(dto)).Returns(entity);

			var service = CreateService(uowMock, mapperMock);

			await Assert.ThrowsAsync<Microsoft.EntityFrameworkCore.DbUpdateException>(() => service.RegisterUserAsync(dto));
		}
	}
}
