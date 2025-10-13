namespace TestsCannabis.TestService.ForgotPasswordServiceTests
{
	public class ForgotPasswordServiceTests_Logic
	{
		private readonly Mock<IRedisService> _redisMock;
		private readonly Mock<IEmailService> _emailMock;
		private readonly Mock<IUserService> _userMock;
		private readonly Mock<IPasswordHasher<User>> _hasherMock;
		private readonly Mock<IMapper> _mapperMock;

		private ForgotPasswordService CreateService()
		{
			return new ForgotPasswordService(
				_redisMock.Object,
				_emailMock.Object,
				_hasherMock.Object,
				_userMock.Object,
				_mapperMock.Object
			);
		}

		public ForgotPasswordServiceTests_Logic()
		{
			_redisMock = new Mock<IRedisService>();
			_emailMock = new Mock<IEmailService>();
			_userMock = new Mock<IUserService>();
			_hasherMock = new Mock<IPasswordHasher<User>>();
			_mapperMock = new Mock<IMapper>();
		}

		[Fact]
		public async Task SendOtpAsync_ShouldGenerateAndSendOtp_WhenNotExistsInRedis()
		{
			// Arrange
			var email = "user@test.com";
			_redisMock.Setup(r => r.GetRedisAsync($"otp:{email}")).ReturnsAsync((string?)null);
			_emailMock.Setup(e => e.SendMailAsync(It.IsAny<EmailMessageParam>())).Returns(Task.CompletedTask);

			var service = CreateService();

			// Act
			await service.SendOtpAsync(email);

			// Assert
			_redisMock.Verify(r => r.SetRedisAsync($"otp:{email}", It.IsAny<string>(), It.Is<TimeSpan?>(t => t!.Value.TotalMinutes == 5)), Times.Once);
			_emailMock.Verify(e => e.SendMailAsync(It.Is<EmailMessageParam>(m => m.To == email)), Times.Once);
		}

		[Fact]
		public async Task ForgotPasswordAsync_ShouldResetPassword_WhenOtpValidAndUserFound()
		{
			// Arrange
			var email = "reset@test.com";
			var otp = "123456";
			var param = new ResetPasswordParam
			{
				Email = email,
				Otp = otp,
				NewPassword = "newPass123"
			};

			var user = new User
			{
				UserId = 1,
				Email = email,
				HashPassword = "old"
			};

			// Mock Redis trả về OTP
			_redisMock.Setup(r => r.GetRedisAsync($"otp:{email}")).ReturnsAsync(otp);

			// Mock UserService trả về user
			_userMock.Setup(u => u.FindUserByEmailAsync(email)).ReturnsAsync(user);

			// Mock PasswordHasher trả về password đã hash
			_hasherMock.Setup(h => h.HashPassword(user, param.NewPassword))
					   .Returns("hashed-pass");

			// Mock AutoMapper trả về UpdateUserDTO từ user
			_mapperMock.Setup(m => m.Map<UpdateUserDTO>(It.IsAny<User>()))
					   .Returns((User u) => new UpdateUserDTO { Password = u.HashPassword! });

			// Mock UpdateAsync trả về UserDTO
			_userMock.Setup(u => u.UpdateAsync(user.UserId, It.IsAny<UpdateUserDTO>()))
					 .ReturnsAsync(new UserDTO { UserId = 1, Email = email });

			// Tạo service với đầy đủ mock
			var service = CreateService();

			// Act
			await service.ForgotPasswordAsync(param);

			// Assert: UpdateAsync được gọi với password đã hash
			_userMock.Verify(u => u.UpdateAsync(
				user.UserId,
				It.Is<UpdateUserDTO>(dto => dto.Password == "hashed-pass")
			), Times.Once);
		}
	}
}

