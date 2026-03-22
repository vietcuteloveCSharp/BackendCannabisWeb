using DTO.DTOs.Shared;
using DTO.TokenDTOs;

namespace Cannabis.Server.Controllers.Shared
{
	[ApiVersion("1.0")]
	[Route("api/v{version:apiVersion}/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _serviceAuth;
		private readonly IRefreshTokenService _refreshTokenService;
		private readonly IForgotPasswordService _forgotPasswordService;
		private readonly IUserService _userService;

		public AuthController(ITokenService serviceToken, IAuthService serviceAuth, IRefreshTokenService refreshTokenService, IForgotPasswordService forgotPasswordService, IUserService userService)
		{
			_serviceAuth = serviceAuth;
			_refreshTokenService = refreshTokenService;
			_forgotPasswordService = forgotPasswordService;
			_userService = userService;
		}
		/// <summary>
		/// Authenticates a user and returns a JWT token upon success.
		/// </summary>
		/// <param name="loginResquestDTO">The login credentials.</param>
		/// <returns>
		/// 200 OK - If authentication is successful.<br/>
		/// 400 Bad Request - If username or password is missing or invalid.
		/// </returns>
		/// <response code="200">Authentication successful. JWT token returned.</response>
		/// <response code="400">Missing or invalid login credentials.</response>
		[HttpPost("login")]
		[AllowAnonymous]
		[ProducesResponseType(typeof(ApiResponse<TokenResultDTO>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> Login([FromBody] LoginResquestDTO loginResquestDTO)
		{
			var tokenDto = await _serviceAuth.LoginAsync(loginResquestDTO);
			// 2. Xử lý Cookie cho Refresh Token (Như đã bàn ở trên)
			AppendRefreshTokenCookie(tokenDto.RefreshToken);
			var tokenResult = new TokenResultDTO
			{
				AccessToken = tokenDto.AccessToken,
				ExpiresAt = DateTime.UtcNow.AddSeconds(tokenDto.ExpiresInSeconds),
				User = tokenDto.User!
			};
			return Ok(ApiResponse<TokenResultDTO>.Ok(tokenResult, "Login successful"));
		}
		/// <summary>
		/// Revokes a specific refresh token.
		/// </summary>
		/// <param name="refreshToken">The refresh token to be revoked.</param>
		/// <returns>
		/// 200 OK - If the token is successfully revoked.<br/>
		/// 400 Bad Request - If the token is missing or empty.
		/// </returns>
		/// <response code="200">Refresh token successfully revoked.</response>
		/// <response code="400">Missing or invalid refresh token.</response>
		[HttpPost("logout")]
		[Authorize] // Bắt buộc phải đăng nhập mới logout được
		[ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
		public async Task<IActionResult> Logout([FromBody] LogoutRequestDTO request)
		{
			// 1. Lấy Refresh Token từ Cookie (Vì ta đã giấu nó ở đây khi Login)
			var refreshToken = Request.Cookies["refreshToken"];

			// 2. Lấy UserId từ Context (Dùng UserId cho nhẹ, không nhất thiết phải ép kiểu cả Entity User)
			if (HttpContext.Items["UserId"] is not int userId)
			{
				return Unauthorized(ApiResponse<object>.Fail("Phiên làm việc không hợp lệ."));
			}

			// 3. Gọi Service để hủy Refresh Token trong DB (nếu có gửi kèm token)
			if (!string.IsNullOrEmpty(refreshToken))
			{
				await _serviceAuth.LogoutAsync(userId, refreshToken);
			}

			// 4. QUAN TRỌNG: Xóa Cookie ở trình duyệt
			Response.Cookies.Delete("refreshToken", new CookieOptions
			{
				HttpOnly = true,
				Secure = true,
				SameSite = SameSiteMode.Strict
			});

			return Ok(ApiResponse<object>.Ok("", "Đăng xuất thành công."));
		}
		[HttpPost("change-password")]
		[Authorize] // Phải login mới đổi được
		public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO dto)
		{
			// Lấy UserId từ HttpContext (do JwtMiddleware của bạn gán vào)
			var user = HttpContext.Items["User"] as DAL.Entities.User;

			await _userService.ChangePasswordAsync(user!.UserId, dto);

			return Ok(ApiResponse<string>.Ok("Đổi mật khẩu thành công."));
		}
		/// <summary>
		/// Revokes all refresh tokens for the currently authenticated user.
		/// </summary>
		/// <returns>
		/// 200 OK - If all tokens are successfully revoked.<br/>
		/// 401 Unauthorized - If the user is not authenticated.
		/// </returns>
		/// <response code = "200" > All refresh tokens successfully revoked.</response>
		/// <response code = "401" > User is not authenticated.</ response >
		//[HttpPost("logout-all")]
		//[Authorize]
		//[ProducesResponseType(typeof(object), 200)]
		//[ProducesResponseType(401)]
		//public async Task<IActionResult> RevokeAllTokens()
		//{
		//	if (HttpContext.Items["User"] is not DAL.Entities.User currentUser)
		//		return Unauthorized("User not authenticated.");

		//	await _refreshTokenService.RevokeAllAsync(currentUser.UserId);

		//	return Ok(new { success = true, message = "All refresh tokens revoked successfully." });
		//}
		/// <summary>
		/// Sends an OTP code to user's email for password reset.
		/// </summary>
		/// <param name="email">User's registered email address.</param>
		/// <returns>
		/// 200 OK - OTP sent successfully.<br/>
		/// 404 Not Found - Email not found.
		/// </returns>
		[HttpPost("forgot-password/send-otp")]
		[AllowAnonymous]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
		public async Task<IActionResult> SendOtp([FromQuery] string email)
		{
			if (string.IsNullOrWhiteSpace(email))
				return BadRequest(ApiResponse<string>.Fail("Email is required."));

			await _forgotPasswordService.SendOtpAsync(email);
			return Ok(ApiResponse<string>.Content("OTP sent successfully. Check your email." ));
		}
		/// <summary>
		/// Resets the user's password using OTP verification.
		/// </summary>
		/// <param name="resetPasswordParam">Object containing email, OTP, and new password.</param>
		/// <returns>
		/// 200 OK - Password reset successful.<br/>
		/// 400 Bad Request - Invalid or expired OTP.
		/// </returns>
		[HttpPost("reset-password-otp")]
		[AllowAnonymous]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		public async Task<IActionResult> ForgotPassword([FromBody] ResetPasswordParam resetPasswordParam)
		{

			await _forgotPasswordService.ForgotPasswordAsync(resetPasswordParam);
			return Ok(ApiResponse<string>.Content("Password has been reset successfully."));
		}
		private void AppendRefreshTokenCookie(string refreshToken)
		{
			var cookieOptions = new CookieOptions
			{
				// 1. Bảo mật: Ngăn JavaScript truy cập cookie (chống XSS)
				HttpOnly = true,

				// 2. Bảo mật: Chỉ gửi cookie qua HTTPS
				// Khi chạy Localhost không có SSL, bạn có thể tạm để false hoặc dùng:
				Secure = true,

				// 3. Bảo mật: Ngăn chặn gửi cookie sang các trang web khác (chống CSRF)
				SameSite = SameSiteMode.Strict,

				// 4. Thời gian sống: Nên khớp với thời gian hết hạn của Refresh Token trong DB
				// Ví dụ: 7 ngày
				Expires = DateTime.UtcNow.AddDays(7)
			};

			// "refreshToken" là key mà Frontend/Middleware sẽ đọc
			Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
		}
	}

}


