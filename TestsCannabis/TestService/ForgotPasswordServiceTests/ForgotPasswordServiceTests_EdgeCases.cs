namespace TestsCannabis.TestService.ForgotPasswordServiceTests
{
	public class ForgotPasswordServiceTests_EdgeCases
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

		public ForgotPasswordServiceTests_EdgeCases()
		{
			_redisMock = new Mock<IRedisService>();
			_emailMock = new Mock<IEmailService>();
			_userMock = new Mock<IUserService>();
			_hasherMock = new Mock<IPasswordHasher<User>>();
			_mapperMock = new Mock<IMapper>();
		}

		// ----- SEND OTP -----

		[Fact]
		public async Task SendOtpAsync_ShouldThrow_WhenEmailIsEmpty()
		{
			// Arrange
			var service = CreateService();

			// Act & Assert
			await Assert.ThrowsAsync<ArgumentException>(() => service.SendOtpAsync(""));
		}

		[Fact]
		public async Task SendOtpAsync_ShouldThrow_WhenOtpAlreadyExistsInRedis()
		{
			// Arrange
			var email = "dup@test.com";
			_redisMock.Setup(r => r.GetRedisAsync($"otp:{email}")).ReturnsAsync("111111");

			var service = CreateService();

			// Act & Assert
			await Assert.ThrowsAsync<InvalidOperationException>(() => service.SendOtpAsync(email));
			_emailMock.Verify(e => e.SendMailAsync(It.IsAny<EmailMessageParam>()), Times.Never);
		}

		// ----- RESET PASSWORD -----

		[Fact]
		public async Task ForgotPasswordAsync_ShouldThrow_WhenOtpNotFoundOrExpired()
		{
			// Arrange
			var email = "otpnotfound@test.com";
			var param = new ResetPasswordParam
			{
				Email = email,
				Otp = "999999",
				NewPassword = "abc123"
			};

			_redisMock.Setup(r => r.GetRedisAsync($"otp:{email}")).ReturnsAsync((string?)null);

			var service = CreateService();

			// Act & Assert
			await Assert.ThrowsAsync<InvalidOperationException>(() => service.ForgotPasswordAsync(param));
		}

		[Fact]
		public async Task ForgotPasswordAsync_ShouldThrow_WhenOtpMismatch()
		{
			// Arrange
			var email = "mismatch@test.com";
			var param = new ResetPasswordParam
			{
				Email = email,
				Otp = "111111",
				NewPassword = "xyz123"
			};

			_redisMock.Setup(r => r.GetRedisAsync($"otp:{email}")).ReturnsAsync("222222");

			var service = CreateService();

			// Act & Assert
			await Assert.ThrowsAsync<InvalidOperationException>(() => service.ForgotPasswordAsync(param));
		}

		[Fact]
		public async Task ForgotPasswordAsync_ShouldThrow_WhenUserNotFound()
		{
			// Arrange
			var email = "nouser@test.com";
			var otp = "333333";
			var param = new ResetPasswordParam
			{
				Email = email,
				Otp = otp,
				NewPassword = "pass123"
			};

			_redisMock.Setup(r => r.GetRedisAsync($"otp:{email}")).ReturnsAsync(otp);
			_userMock.Setup(u => u.FindUserByEmailAsync(email)).ReturnsAsync((User?)null);

			var service = CreateService();

			// Act & Assert
			await Assert.ThrowsAsync<NotFoundException>(() => service.ForgotPasswordAsync(param));
		}

		[Fact]
		public async Task ForgotPasswordAsync_ShouldThrow_WhenUpdateFails()
		{
			// Arrange
			var email = "failupdate@test.com";
			var otp = "444444";
			var param = new ResetPasswordParam
			{
				Email = email,
				Otp = otp,
				NewPassword = "newpass"
			};

			var user = new User { UserId = 10, Email = email };

			_redisMock.Setup(r => r.GetRedisAsync($"otp:{email}")).ReturnsAsync(otp);
			_userMock.Setup(u => u.FindUserByEmailAsync(email)).ReturnsAsync(user);
			_hasherMock.Setup(h => h.HashPassword(user, param.NewPassword)).Returns("hashed-fail");

			// UpdateAsync trả về null (mô phỏng lỗi)
			_userMock.Setup(u => u.UpdateAsync(user.UserId, It.IsAny<UpdateUserDTO>()))
					 .ReturnsAsync((UserDTO?)null);

			var service = CreateService();

			// Act & Assert
			await Assert.ThrowsAsync<InvalidOperationException>(() => service.ForgotPasswordAsync(param));
		}
	}
}

