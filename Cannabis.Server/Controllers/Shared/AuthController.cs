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

		public AuthController(ITokenService serviceToken, IAuthService serviceAuth, IRefreshTokenService refreshTokenService, IForgotPasswordService forgotPasswordService)
		{
			_serviceAuth = serviceAuth;
			_refreshTokenService = refreshTokenService;
			_forgotPasswordService = forgotPasswordService;
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
		[ProducesResponseType(typeof(object), 200)]
		[ProducesResponseType(400)]
		public async Task<IActionResult> LoginAccount([FromBody] LoginResquestDTO loginResquestDTO)
		{
			if (string.IsNullOrEmpty(loginResquestDTO.Username) || string.IsNullOrEmpty(loginResquestDTO.Password))
			{
				return BadRequest(ApiResponse<string>.Fail("Username and password are required."));
			}

			var tokenDto = await _serviceAuth.LoginAsync(loginResquestDTO);
			var tokenResult = new TokenResultDTO
			{
				AccessToken = tokenDto.AccessToken,
				RefreshToken = tokenDto.RefreshToken,
				ExpiresIn = (int)(tokenDto.Expiration - DateTime.UtcNow).TotalSeconds,
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
		[Authorize]
		[ProducesResponseType(typeof(object), 200)]
		[ProducesResponseType(400)]
		public async Task<IActionResult> RevokeToken([FromBody] string refreshToken)
		{
			if (string.IsNullOrWhiteSpace(refreshToken))
			{
				return BadRequest(ApiResponse<string>.Fail("Refresh token is required."));
			}

			await _refreshTokenService.RevokeTokenAsync(refreshToken);

			return Ok(ApiResponse<string?>.Content("Refresh token successfully revoked."));
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
		[HttpPost("logout-all")]
		[Authorize]
		[ProducesResponseType(typeof(object), 200)]
		[ProducesResponseType(401)]
		public async Task<IActionResult> RevokeAllTokens()
		{
			if (HttpContext.Items["User"] is not DAL.Entities.User currentUser)
				return Unauthorized("User not authenticated.");

			await _refreshTokenService.RevokeAllAsync(currentUser.UserId);

			return Ok(new { success = true, message = "All refresh tokens revoked successfully." });
		}
		/// <summary>
		/// Sends an OTP code to user's email for password reset.
		/// </summary>
		/// <param name="email">User's registered email address.</param>
		/// <returns>
		/// 200 OK - OTP sent successfully.<br/>
		/// 404 Not Found - Email not found.
		/// </returns>
		[HttpPost("send-otp")]
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
		[HttpPost("forgot-password")]
		[AllowAnonymous]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		public async Task<IActionResult> ForgotPassword([FromBody] ResetPasswordParam resetPasswordParam)
		{
			if (!ModelState.IsValid)
				return this.ValidateModelState();

			await _forgotPasswordService.ForgotPasswordAsync(resetPasswordParam);
			return Ok(ApiResponse<string>.Content("Password reset successfully."));
		}
	}
}


