using DTO.DTOs.User.Users;
using Service.Services.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsCannabis.TestService.UserTests
{
	public class UserServiceTests_Register_EdgeCase
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
		public async Task RegisterUser_ShouldThrow_WhenDtoNull()
		{
			var service = CreateService();
			await Assert.ThrowsAsync<ArgumentNullException>(() => service.RegisterUserAsync(null!));
		}

		[Fact]
		public async Task RegisterUser_ShouldThrow_WhenPasswordNull()
		{
			var dto = new CreateUserDTO { Username = "u", Password = null!, Email = "a@a.com" };
			var service = CreateService();
			await Assert.ThrowsAsync<ArgumentNullException>(() => service.RegisterUserAsync(dto));
		}

		[Fact]
		public async Task RegisterUser_ShouldThrow_WhenMapperReturnsNull()
		{
			var dto = new CreateUserDTO { Username = "u", Password = "PassA1", Email = "a@a.com" };
			var role = new Role { RoleId = 2, RoleName = ERoleName.User };

			var uow = new Mock<IUnitOfWork>();
			uow.Setup(u => u.Users.EmailExistsAsync(It.IsAny<string>())).ReturnsAsync(false);
			uow.Setup(u => u.Users.UserNameExistsAsync(It.IsAny<string>())).ReturnsAsync(false);
			uow.Setup(u => u.Roles.GetByNameAsync("User")).ReturnsAsync(role);

			var mapper = new Mock<IMapper>();
			mapper.Setup(m => m.Map<User>(dto)).Returns((User?)null);

			var service = CreateService(uow, mapper);
			await Assert.ThrowsAsync<NullReferenceException>(() => service.RegisterUserAsync(dto));
		}

		[Fact]
		public async Task RegisterUser_ShouldThrow_WhenHashPasswordFails()
		{
			var dto = new CreateUserDTO { Username = "a", Password = "StrongA1", Email = "a@a.com" };
			var role = new Role { RoleId = 2, RoleName = ERoleName.User };
			var userEntity = new User { Username = "a" };

			var uow = new Mock<IUnitOfWork>();
			uow.Setup(u => u.Users.EmailExistsAsync(dto.Email)).ReturnsAsync(false);
			uow.Setup(u => u.Users.UserNameExistsAsync(dto.Username)).ReturnsAsync(false);
			uow.Setup(u => u.Roles.GetByNameAsync("User")).ReturnsAsync(role);
			uow.Setup(u => u.Users.AddAsync(It.IsAny<User>())).ReturnsAsync(userEntity);

			var mapper = new Mock<IMapper>();
			mapper.Setup(m => m.Map<User>(dto)).Returns(userEntity);

			var passwordHasher = new Mock<IPasswordHasher<User>>();
			passwordHasher.Setup(p => p.HashPassword(userEntity, dto.Password))
				.Throws(new Exception("Hash fail"));

			var service = CreateService(uow, mapper, passwordHasher.Object);
			await Assert.ThrowsAsync<Exception>(() => service.RegisterUserAsync(dto));
		}

		[Fact]
		public async Task RegisterUser_ShouldThrow_WhenAddAsyncFails()
		{
			var dto = new CreateUserDTO { Username = "a", Password = "PassA1", Email = "a@a.com" };
			var role = new Role { RoleId = 2, RoleName = ERoleName.User };
			var userEntity = new User { Username = "a" };

			var uow = new Mock<IUnitOfWork>();
			uow.Setup(u => u.Users.EmailExistsAsync(dto.Email)).ReturnsAsync(false);
			uow.Setup(u => u.Users.UserNameExistsAsync(dto.Username)).ReturnsAsync(false);
			uow.Setup(u => u.Roles.GetByNameAsync("User")).ReturnsAsync(role);
			uow.Setup(u => u.Users.AddAsync(It.IsAny<User>())).ThrowsAsync(new Exception("Insert fail"));

			var mapper = new Mock<IMapper>();
			mapper.Setup(m => m.Map<User>(dto)).Returns(userEntity);

			var service = CreateService(uow, mapper);
			await Assert.ThrowsAsync<Exception>(() => service.RegisterUserAsync(dto));
		}
	}
}

