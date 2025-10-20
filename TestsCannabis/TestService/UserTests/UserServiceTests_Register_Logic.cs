using DTO.DTOs.User.Users;
using Service.Services.UserManagement;


namespace TestsCannabis.TestService.UserTests
{
	public class UserServiceTests_Register_Logic
	{
		private UserService CreateService(
			Mock<IUnitOfWork>? uowMock = null,
			Mock<IMapper>? mapperMock = null,
			IPasswordHasher<User>? passwordHasher = null)
		{
			return new UserService(
				uowMock?.Object ?? new Mock<IUnitOfWork>().Object,
				mapperMock?.Object ?? new Mock<IMapper>().Object,
				passwordHasher ?? new PasswordHasher<User>()
			);
		}

		[Fact]
		public async Task RegisterUser_ShouldAddUser_WhenDataValid()
		{
			var dto = new CreateUserDTO
			{
				Username = "user1",
				Password = "StrongPassA",
				Email = "user@test.com",
				Name = "User"
			};

			var role = new Role { RoleId = 2, RoleName = ERoleName.User };

			var uow = new Mock<IUnitOfWork>();
			uow.Setup(u => u.Users.EmailExistsAsync(dto.Email)).ReturnsAsync(false);
			uow.Setup(u => u.Users.UserNameExistsAsync(dto.Username)).ReturnsAsync(false);
			uow.Setup(u => u.Roles.GetByNameAsync("User")).ReturnsAsync(role);
			uow.Setup(u => u.Users.AddAsync(It.IsAny<User>())).ReturnsAsync((User u) => u);

			var mapper = new Mock<IMapper>();
			mapper.Setup(m => m.Map<User>(dto)).Returns(new User { Username = dto.Username, Email = dto.Email });
			mapper.Setup(m => m.Map<UserDTO>(It.IsAny<User>())).Returns(new UserDTO { Username = dto.Username });

			var service = CreateService(uow, mapper);

			var result = await service.RegisterUserAsync(dto);

			Assert.NotNull(result);
			Assert.Equal("user1", result.Username);
			uow.Verify(u => u.SaveChangesAsync(), Times.Once);
		}

		[Fact]
		public async Task RegisterUser_ShouldThrow_WhenEmailExists()
		{
			var dto = new CreateUserDTO { Email = "dup@test.com", Username = "a", Password = "PassA1" };

			var uow = new Mock<IUnitOfWork>();
			uow.Setup(u => u.Users.EmailExistsAsync(dto.Email)).ReturnsAsync(true);

			var service = CreateService(uow);

			await Assert.ThrowsAsync<InvalidOperationException>(() => service.RegisterUserAsync(dto));
		}

		[Fact]
		public async Task RegisterUser_ShouldThrow_WhenUsernameExists()
		{
			var dto = new CreateUserDTO { Email = "new@test.com", Username = "dup", Password = "PassA1" };

			var uow = new Mock<IUnitOfWork>();
			uow.Setup(u => u.Users.EmailExistsAsync(dto.Email)).ReturnsAsync(false);
			uow.Setup(u => u.Users.UserNameExistsAsync(dto.Username)).ReturnsAsync(true);

			var service = CreateService(uow);

			await Assert.ThrowsAsync<InvalidOperationException>(() => service.RegisterUserAsync(dto));
		}

		[Fact]
		public async Task RegisterUser_ShouldThrow_WhenRoleUserNotFound()
		{
			var dto = new CreateUserDTO { Email = "e@x.com", Username = "a", Password = "PassA1" };

			var uow = new Mock<IUnitOfWork>();
			uow.Setup(u => u.Users.EmailExistsAsync(dto.Email)).ReturnsAsync(false);
			uow.Setup(u => u.Users.UserNameExistsAsync(dto.Username)).ReturnsAsync(false);
			uow.Setup(u => u.Roles.GetByNameAsync("User")).ReturnsAsync((Role?)null);

			var service = CreateService(uow);

			await Assert.ThrowsAsync<InvalidOperationException>(() => service.RegisterUserAsync(dto));
		}

		[Fact]
		public async Task RegisterUser_ShouldThrow_WhenSaveChangesFails()
		{
			var dto = new CreateUserDTO { Email = "a@x.com", Username = "a", Password = "PassA1" };
			var role = new Role { RoleId = 2, RoleName = ERoleName.User };
			var entity = new User { Username = "a", Email = "a@x.com" };

			var uow = new Mock<IUnitOfWork>();
			uow.Setup(u => u.Users.EmailExistsAsync(dto.Email)).ReturnsAsync(false);
			uow.Setup(u => u.Users.UserNameExistsAsync(dto.Username)).ReturnsAsync(false);
			uow.Setup(u => u.Roles.GetByNameAsync("User")).ReturnsAsync(role);
			uow.Setup(u => u.Users.AddAsync(It.IsAny<User>())).ReturnsAsync(entity);
			uow.Setup(u => u.SaveChangesAsync()).ThrowsAsync(new Exception("DB error"));

			var mapper = new Mock<IMapper>();
			mapper.Setup(m => m.Map<User>(dto)).Returns(entity);

			var service = CreateService(uow, mapper);

			await Assert.ThrowsAsync<Exception>(() => service.RegisterUserAsync(dto));
		}
	}
}

