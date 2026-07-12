using Service.Interfaces.Shared;
using System.Security.Claims;

namespace Cannabis.Server.Middleware
{
	public class AdminAuthorizeMiddleware
	{
		private readonly RequestDelegate _next;

		public AdminAuthorizeMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context, ITokenService tokenService)
		{
			var path = context.Request.Path.Value ?? "";

			// Bỏ qua các API public hoặc API Client
			if (!path.Contains("/admin/", StringComparison.OrdinalIgnoreCase) || 
				path.Contains("/admin/auth/login", StringComparison.OrdinalIgnoreCase) || 
				path.Contains("/admin/auth/refresh-token", StringComparison.OrdinalIgnoreCase))
			{
				await _next(context);
				return;
			}

			// Trích xuất Bearer token từ Header Authorization
			var authHeader = context.Request.Headers["Authorization"].ToString();
			if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
			{
				context.Response.StatusCode = StatusCodes.Status401Unauthorized;
				context.Response.ContentType = "application/json";
				await context.Response.WriteAsJsonAsync(ApiResponse<object>.Fail("Yêu cầu mã xác thực Bearer token."));
				return;
			}

			var token = authHeader.Substring("Bearer ".Length).Trim();
			var principal = tokenService.ValidateToken(token);
			if (principal == null)
			{
				context.Response.StatusCode = StatusCodes.Status401Unauthorized;
				context.Response.ContentType = "application/json";
				await context.Response.WriteAsJsonAsync(ApiResponse<object>.Fail("Token không hợp lệ hoặc đã hết hạn."));
				return;
			}

			// Kiểm tra claim isAdmin hoặc Role là Admin
			var isAdminClaim = principal.FindFirst("isAdmin")?.Value;
			var roleClaim = principal.FindFirst(ClaimTypes.Role)?.Value;

			bool isAdmin = (isAdminClaim != null && string.Equals(isAdminClaim, "true", StringComparison.OrdinalIgnoreCase))
			               || string.Equals(roleClaim, "Admin", StringComparison.OrdinalIgnoreCase);

			if (!isAdmin)
			{
				context.Response.StatusCode = StatusCodes.Status403Forbidden;
				context.Response.ContentType = "application/json";
				await context.Response.WriteAsJsonAsync(ApiResponse<object>.Fail("Bạn không có quyền truy cập tài nguyên này."));
				return;
			}

			// Gắn thông tin admin giải mã vào Request context
			context.Items["Admin"] = principal;
			var adminIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier) ?? principal.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub);
			if (adminIdClaim != null && int.TryParse(adminIdClaim.Value, out int adminId))
			{
				context.Items["AdminId"] = adminId;
			}

			context.User = principal;

			await _next(context);
		}
	}
}
