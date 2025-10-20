using DTO.DTOs.Admin.Admins;
using DTO.DTOs.User.Users;
using Service.Services.AdminManagement;


namespace TestsCannabis.TestService.AdminTest
{
	public class AdminServiceTests_Register_Logic
	{
		private AdminService CreateService(
		   Mock<IUnitOfWork>? uowMock = null,
		   Mock<IMapper>? mapperMock = null,
		   IPasswordHasher<User>? passwordHasher = null)
		{
			return new AdminService(
				uowMock?.Object ?? new Mock<IUnitOfWork>().Object,
				mapperMock?.Object ?? new Mock<IMapper>().Object,
				passwordHasher ?? new PasswordHasher<User>()
			);
		}

		[Fact]
		public async Task RegisterAdmin_ShouldAddUser_WhenDataValid()
		{
			// Arrange
			var dto = new CreateAdminDTO
			{
				Username = "admin1",
				Password = "StrongPassA",
				Email = "admin@test.com",
				RoleId = 1,
				Name = "Admin"
			};

			var uow = new Mock<IUnitOfWork>();
			uow.Setup(u => u.Users.EmailExistsAsync(dto.Email)).ReturnsAsync(false);
			uow.Setup(u => u.Users.UserNameExistsAsync(dto.Username)).ReturnsAsync(false);
			uow.Setup(u => u.Roles.GetByIdAsync(dto.RoleId)).ReturnsAsync(new Role { RoleId = 1, RoleName = ERoleName.Admin });
			uow.Setup(u => u.Users.AddAsync(It.IsAny<User>())).ReturnsAsync((User u) => u);

			var mapper = new Mock<IMapper>();
			mapper.Setup(m => m.Map<User>(dto)).Returns(new User { Username = dto.Username, Email = dto.Email });
			mapper.Setup(m => m.Map<UserDTO>(It.IsAny<User>())).Returns(new UserDTO { Username = dto.Username });

			var service = CreateService(uow, mapper);

			// Act
			var result = await service.RegisterAdminAsync(dto);

			// Assert
			Assert.NotNull(result);
			Assert.Equal("admin1", result.Username);
			uow.Verify(u => u.SaveChangesAsync(), Times.Once);
		}

		[Fact]
		public async Task RegisterAdmin_ShouldThrow_WhenEmailExists()
		{
			var dto = new CreateAdminDTO { Email = "dup@test.com", Username = "a", Password = "PassA1", RoleId = 1 };

			var uow = new Mock<IUnitOfWork>();
			uow.Setup(u => u.Users.EmailExistsAsync(dto.Email)).ReturnsAsync(true);

			var service = CreateService(uow);

			await Assert.ThrowsAsync<InvalidOperationException>(() => service.RegisterAdminAsync(dto));
		}

		[Fact]
		public async Task RegisterAdmin_ShouldThrow_WhenUsernameExists()
		{
			var dto = new CreateAdminDTO { Email = "new@test.com", Username = "dup", Password = "PassA1", RoleId = 1 };

			var uow = new Mock<IUnitOfWork>();
			uow.Setup(u => u.Users.EmailExistsAsync(dto.Email)).ReturnsAsync(false);
			uow.Setup(u => u.Users.UserNameExistsAsync(dto.Username)).ReturnsAsync(true);

			var service = CreateService(uow);

			await Assert.ThrowsAsync<InvalidOperationException>(() => service.RegisterAdminAsync(dto));
		}

		[Fact]
		public async Task RegisterAdmin_ShouldThrow_WhenRoleInvalid()
		{
			var dto = new CreateAdminDTO { Email = "e@x.com", Username = "a", Password = "PassA1", RoleId = 9 };

			var uow = new Mock<IUnitOfWork>();
			uow.Setup(u => u.Users.EmailExistsAsync(dto.Email)).ReturnsAsync(false);
			uow.Setup(u => u.Users.UserNameExistsAsync(dto.Username)).ReturnsAsync(false);
			uow.Setup(u => u.Roles.GetByIdAsync(dto.RoleId)).ReturnsAsync((Role?)null);

			var service = CreateService(uow);

			await Assert.ThrowsAsync<InvalidOperationException>(() => service.RegisterAdminAsync(dto));
		}

		[Fact]
		public async Task RegisterAdmin_ShouldThrow_WhenSaveChangesFails()
		{
			var dto = new CreateAdminDTO { Email = "a@x.com", Username = "a", Password = "PassA1", RoleId = 1 };

			var userEntity = new User { Username = "a", Email = "a@x.com" };

			var uow = new Mock<IUnitOfWork>();
			uow.Setup(u => u.Users.EmailExistsAsync(dto.Email)).ReturnsAsync(false);
			uow.Setup(u => u.Users.UserNameExistsAsync(dto.Username)).ReturnsAsync(false);
			uow.Setup(u => u.Roles.GetByIdAsync(dto.RoleId)).ReturnsAsync(new Role { RoleId = 1 });
			uow.Setup(u => u.Users.AddAsync(It.IsAny<User>())).ReturnsAsync(userEntity);
			uow.Setup(u => u.SaveChangesAsync()).ThrowsAsync(new Exception("DB Error"));

			var mapper = new Mock<IMapper>();
			mapper.Setup(m => m.Map<User>(dto)).Returns(userEntity);

			var service = CreateService(uow, mapper);

			await Assert.ThrowsAsync<Exception>(() => service.RegisterAdminAsync(dto));
		}
	}
}

