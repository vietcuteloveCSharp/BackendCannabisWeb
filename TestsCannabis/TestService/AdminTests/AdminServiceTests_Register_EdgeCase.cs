using DTO.DTOs.Admin.Admins;
using DTO.DTOs.User.Users;
using Service.Services.AdminManagement;

namespace TestsCannabis.TestService.AdminTest
{
	public  class AdminServiceTests_Register_EdgeCase
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
		public async Task RegisterAdmin_ShouldThrow_WhenDtoNull()
		{
			var service = CreateService();
			await Assert.ThrowsAsync<ArgumentNullException>(() => service.RegisterAdminAsync(null!));
		}

		[Fact]
		public async Task RegisterAdmin_ShouldThrow_WhenPasswordNull()
		{
			var dto = new CreateAdminDTO { Username = "u", Password = null!, Email = "a@a.com", RoleId = 1 };

			var service = CreateService();
			await Assert.ThrowsAsync<ArgumentNullException>(() => service.RegisterAdminAsync(dto));
		}

		[Fact]
		public async Task RegisterAdmin_ShouldThrow_WhenMapperReturnsNullUser()
		{
			var dto = new CreateAdminDTO { Username = "u", Password = "PassA1", Email = "a@a.com", RoleId = 1 };

			var uow = new Mock<IUnitOfWork>();
			uow.Setup(u => u.Users.EmailExistsAsync(It.IsAny<string>())).ReturnsAsync(false);
			uow.Setup(u => u.Users.UserNameExistsAsync(It.IsAny<string>())).ReturnsAsync(false);
			uow.Setup(u => u.Roles.GetByIdAsync(1)).ReturnsAsync(new Role { RoleId = 1 });

			var mapper = new Mock<IMapper>();
			mapper.Setup(m => m.Map<User>(dto)).Returns((User?)null);

			var service = CreateService(uow, mapper);
			await Assert.ThrowsAsync<NullReferenceException>(() => service.RegisterAdminAsync(dto));
		}

		[Fact]
		public async Task RegisterAdmin_ShouldThrow_WhenHashPasswordFails()
		{
			var dto = new CreateAdminDTO { Username = "a", Password = "StrongA1", Email = "a@a.com", RoleId = 1 };
			var userEntity = new User { Username = "a" };

			var uow = new Mock<IUnitOfWork>();
			uow.Setup(u => u.Users.EmailExistsAsync(dto.Email)).ReturnsAsync(false);
			uow.Setup(u => u.Users.UserNameExistsAsync(dto.Username)).ReturnsAsync(false);
			uow.Setup(u => u.Roles.GetByIdAsync(dto.RoleId)).ReturnsAsync(new Role { RoleId = 1 });
			uow.Setup(u => u.Users.AddAsync(It.IsAny<User>())).ReturnsAsync(userEntity);

			var mapper = new Mock<IMapper>();
			mapper.Setup(m => m.Map<User>(dto)).Returns(userEntity);

			var passwordHasher = new Mock<IPasswordHasher<User>>();
			passwordHasher.Setup(p => p.HashPassword(userEntity, dto.Password))
				.Throws(new Exception("Hash fail"));

			var service = CreateService(uow, mapper, passwordHasher.Object);

			await Assert.ThrowsAsync<Exception>(() => service.RegisterAdminAsync(dto));
		}

		[Fact]
		public async Task RegisterAdmin_ShouldThrow_WhenAddAsyncThrows()
		{
			var dto = new CreateAdminDTO { Username = "a", Password = "PassA1", Email = "a@a.com", RoleId = 1 };
			var userEntity = new User { Username = "a" };

			var uow = new Mock<IUnitOfWork>();
			uow.Setup(u => u.Users.EmailExistsAsync(dto.Email)).ReturnsAsync(false);
			uow.Setup(u => u.Users.UserNameExistsAsync(dto.Username)).ReturnsAsync(false);
			uow.Setup(u => u.Roles.GetByIdAsync(dto.RoleId)).ReturnsAsync(new Role { RoleId = 1 });
			uow.Setup(u => u.Users.AddAsync(It.IsAny<User>())).ThrowsAsync(new Exception("Insert fail"));

			var mapper = new Mock<IMapper>();
			mapper.Setup(m => m.Map<User>(dto)).Returns(userEntity);

			var service = CreateService(uow, mapper);
			await Assert.ThrowsAsync<Exception>(() => service.RegisterAdminAsync(dto));
		}

		[Fact]
		public async Task RegisterAdmin_ShouldThrow_WhenMapperBackToUserDTONull()
		{
			var dto = new CreateAdminDTO { Username = "a", Password = "PassA1", Email = "a@a.com", RoleId = 1 };
			var userEntity = new User { Username = "a" };

			var uow = new Mock<IUnitOfWork>();
			uow.Setup(u => u.Users.EmailExistsAsync(dto.Email)).ReturnsAsync(false);
			uow.Setup(u => u.Users.UserNameExistsAsync(dto.Username)).ReturnsAsync(false);
			uow.Setup(u => u.Roles.GetByIdAsync(dto.RoleId)).ReturnsAsync(new Role { RoleId = 1 });
			uow.Setup(u => u.Users.AddAsync(It.IsAny<User>())).ReturnsAsync(userEntity);

			var mapper = new Mock<IMapper>();
			mapper.Setup(m => m.Map<User>(dto)).Returns(userEntity);
			mapper.Setup(m => m.Map<UserDTO>(userEntity)).Returns((UserDTO?)null);

			var service = CreateService(uow, mapper);
			var result = await service.RegisterAdminAsync(dto);

			Assert.Null(result);
		}
	}
}
