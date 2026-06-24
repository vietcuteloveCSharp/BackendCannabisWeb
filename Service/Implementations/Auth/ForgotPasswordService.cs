

namespace Service.Implementations.Auth
{
	public class ForgotPasswordService : IForgotPasswordService
	{
		private readonly IRedisService _redisService;
		private readonly IEmailService _emailService;
		private readonly IUserService _userService;
		private readonly IMapper _mapper;
		private readonly IPasswordHasher<User> _passwordHasher;

		public ForgotPasswordService(IRedisService redisService, IEmailService emailService, IPasswordHasher<User> passwordHasher, IUserService userService, IMapper mapper)
		{
			_redisService = redisService;
			_emailService = emailService;
			_passwordHasher = passwordHasher;
			_userService = userService;
			_mapper = mapper;
		}
		//đặt lại mật khẩu
		public async Task ForgotPasswordAsync(ResetPasswordParam resetPasswordParam)
		{
			//get key 
			var key = $"otp:{resetPasswordParam.Email}";
			// 1. Lấy OTP từ Redis
			var storedOtp = await _redisService.GetRedisAsync($"otp:{resetPasswordParam.Email}");
			if (storedOtp == null || storedOtp != resetPasswordParam.Otp)
			{
				throw new InvalidOperationException("Invalid or expired OTP");
			}
			var user = await _userService.FindUserByEmailAsync(resetPasswordParam.Email) ?? throw new NotFoundException($"User with email {resetPasswordParam.Email} not found.");
		
			// 2. Hash mật khẩu mới
			var hashedPassword = _passwordHasher.HashPassword(user,resetPasswordParam.NewPassword);
			user.PasswordHash = hashedPassword;
			var updateUserDto = _mapper.Map<UpdateUserDTO>(user);
			// 3. Cập nhật mật khẩu trong DB
			var result= await _userService.UpdateAsync(user.Id,updateUserDto);
			if (result==null)
			{
				throw new InvalidOperationException("Failed to update password.");
			}
			// 5. Xóa OTP trong Redis (chống dùng lại)
			await _redisService.RemoveRedisAsync(key);


		}
		//gửi otp
		public async Task SendOtpAsync(string email)
		{
			if (string.IsNullOrWhiteSpace(email))
				throw new ArgumentException("Email is required.", nameof(email));
			var user = await _userService.FindUserByEmailAsync(email);
			// Nếu không có user, chúng ta vẫn "giả vờ" thực hiện các bước tiếp theo
			if (user == null)
			{
				// Log nội bộ để DEV biết, nhưng không trả về lỗi cho Client
				// _logger.LogInformation($"Yêu cầu OTP cho email không tồn tại: {email}");
				return; // Kết thúc hàm tại đây, FE vẫn nhận được 200 OK
			}
			var key = $"otp:{email}";
			// 🔹 Kiểm tra xem đã có OTP đang tồn tại chưa
			var existingOtp = await _redisService.GetRedisAsync(key);
			if (existingOtp != null)
			{
				throw new InvalidOperationException("OTP already sent. Please wait before requesting another one.");
			}
			// 1. Sinh OTP ngẫu nhiên
			var otp = new Random().Next(100000, 999999).ToString();
			// lưu vào redis với thời gian 5p
			await _redisService.SetRedisAsync($"otp:{email}", otp, TimeSpan.FromMinutes(5));
			// 3. Gửi email
			var message = new EmailMessageParam
			{
				To = email,
				Subject = "Password Reset OTP",
				Body = $"Your OTP is <b>{otp}</b>. It will expire in 5 minutes."
			};
			await _emailService.SendMailAsync(message);
		}
	}
}
