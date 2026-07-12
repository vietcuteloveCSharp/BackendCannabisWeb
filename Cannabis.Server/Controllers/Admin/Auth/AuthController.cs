using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Shared.Common.Response;
using Shared.DTOs.DTO.Internal;
using Shared.DTOs.Options;
using System.Security.Claims;
using Service.Interfaces.Auth.Internal;

namespace Cannabis.Server.Controllers.Admin.Auth
{
	[ApiVersion("1.0")]
	[Route("api/v{version:apiVersion}/admin/auth")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IStaffAuthService _staffAuthService;
		private readonly IStaffRefreshTokenService _staffRefreshTokenService;
		private readonly JwtSettings _jwtSettings;

		public AuthController(
			IStaffAuthService staffAuthService,
			IStaffRefreshTokenService staffRefreshTokenService,
			IOptions<JwtSettings> jwtSettings)
		{
			_staffAuthService = staffAuthService ?? throw new ArgumentNullException(nameof(staffAuthService));
			_staffRefreshTokenService = staffRefreshTokenService ?? throw new ArgumentNullException(nameof(staffRefreshTokenService));
			_jwtSettings = jwtSettings.Value ?? throw new ArgumentNullException(nameof(jwtSettings));
		}

		/// <summary>
		/// Đăng nhập dành cho tài khoản Admin/Staff
		/// </summary>
		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginRequest request)
		{
			if (request == null)
			{
				return BadRequest(ApiResponse<object>.Fail("Dữ liệu đầu vào không hợp lệ."));
			}

			var tokenResponse = await _staffAuthService.LoginAsync(request);

			// Lưu Refresh Token vào secure, HttpOnly, Secure, SameSite=Strict cookie
			var cookieOptions = new CookieOptions
			{
				HttpOnly = true,
				Secure = true, // HTTPS-only
				SameSite = SameSiteMode.Strict,
				Expires = DateTimeOffset.UtcNow.AddDays(_jwtSettings.RefreshTokenExpiryDays)
			};
			Response.Cookies.Append("refreshToken", tokenResponse.RefreshToken, cookieOptions);

			return Ok(ApiResponse<TokenResponse>.Ok(tokenResponse, "Đăng nhập thành công."));
		}

		/// <summary>
		/// Làm mới Access Token (Token Rotation Mode)
		/// </summary>
		[HttpPost("refresh-token")]
		public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest? requestBody)
		{
			// 1. Lấy refresh token từ cookie
			if (!Request.Cookies.TryGetValue("refreshToken", out string? oldRefreshToken) || string.IsNullOrEmpty(oldRefreshToken))
			{
				oldRefreshToken = requestBody?.OldRefreshToken;
			}

			if (string.IsNullOrEmpty(oldRefreshToken))
			{
				return BadRequest(ApiResponse<object>.Fail("Thiếu Refresh Token hợp lệ."));
			}

			// 2. Lấy Access Token đã hết hạn từ Header Authorization hoặc Body
			var authHeader = Request.Headers["Authorization"].ToString();
			string? expiredAccessToken = null;
			if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
			{
				expiredAccessToken = authHeader.Substring("Bearer ".Length).Trim();
			}
			else
			{
				expiredAccessToken = requestBody?.ExpiredAccessToken;
			}

			if (string.IsNullOrEmpty(expiredAccessToken))
			{
				return BadRequest(ApiResponse<object>.Fail("Thiếu Access Token đã hết hạn."));
			}

			var rotationRequest = new RefreshTokenRequest
			{
				ExpiredAccessToken = expiredAccessToken,
				OldRefreshToken = oldRefreshToken
			};

			var tokenResponse = await _staffAuthService.RefreshTokenAsync(rotationRequest);

			// Thiết lập lại cookie mới (Token Rotation)
			var cookieOptions = new CookieOptions
			{
				HttpOnly = true,
				Secure = true,
				SameSite = SameSiteMode.Strict,
				Expires = DateTimeOffset.UtcNow.AddDays(_jwtSettings.RefreshTokenExpiryDays)
			};
			Response.Cookies.Append("refreshToken", tokenResponse.RefreshToken, cookieOptions);

			return Ok(ApiResponse<TokenResponse>.Ok(tokenResponse, "Làm mới token thành công."));
		}

		/// <summary>
		/// Đăng xuất khỏi hệ thống
		/// </summary>
		[HttpPost("logout")]
		public async Task<IActionResult> Logout()
		{
			// Trích xuất refresh token để hủy phiên
			if (!Request.Cookies.TryGetValue("refreshToken", out string? oldRefreshToken) || string.IsNullOrEmpty(oldRefreshToken))
			{
				return BadRequest(ApiResponse<object>.Fail("Phiên làm việc không hợp lệ hoặc đã đăng xuất."));
			}

			// Lấy danh tính người dùng
			int staffId = 0;
			var staffIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub);
			if (staffIdClaim != null)
			{
				int.TryParse(staffIdClaim.Value, out staffId);
			}

			if (staffId == 0)
			{
				// Thử tìm StaffId qua Refresh Token nếu Access Token đã hết hạn hoặc không được truyền
				var tokenQuery = new TokenQuery { RefreshToken = oldRefreshToken, IncludeRevoked = true };
				var tokenEntity = await _staffRefreshTokenService.GetTokenAsync(tokenQuery);
				if (tokenEntity != null)
				{
					staffId = tokenEntity.StaffId;
				}
			}

			if (staffId == 0)
			{
				return BadRequest(ApiResponse<object>.Fail("Không xác định được phiên làm việc của tài khoản."));
			}

			var logoutRequest = new LogoutRequest
			{
				StaffId = staffId,
				RefreshTokenValue = oldRefreshToken
			};

			await _staffAuthService.LogoutAsync(logoutRequest);

			// Xóa cookie Refresh Token
			Response.Cookies.Delete("refreshToken");

			return Ok(ApiResponse<object>.Ok(null, "Đăng xuất thành công."));
		}
	}
}
