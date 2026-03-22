using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Cannabis.Server.DependencyInjection
{
	public static class JwtExtensions
	{
		public static IServiceCollection AddJwtAuth(this IServiceCollection services, IConfiguration config)
		{
			// bind config section "Jwt" -> JwtSettings
			services.Configure<JwtSettings>(config.GetSection("Jwt"));

			// Lấy giá trị ngay bây giờ từ config (để dùng khi cấu hình TokenValidationParameters)
			var jwtSettings = config.GetSection("Jwt").Get<JwtSettings>()
				?? throw new InvalidOperationException("Jwt section is missing in configuration.");

			if (string.IsNullOrWhiteSpace(jwtSettings.Key))
				throw new InvalidOperationException("JWT Key is missing in configuration.");
			// Xóa map mặc định để JWT trả về key ngắn gọn
			JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
			JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();

			// đăng ký JwtBearer và lấy options từ DI
			services.AddAuthentication(options => {
						options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
							options.DefaultChallengeScheme= JwtBearerDefaults.AuthenticationScheme;
						})
			.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,options =>
			{
						// cấu hình token validation
				options.RequireHttpsMetadata = false;
				options.SaveToken = true;

				options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = jwtSettings.Issuer,
					ValidAudience = jwtSettings.Audience,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
					ClockSkew = TimeSpan.Zero // loại bỏ độ trễ thời gian
				};
				// cấu hình event cho product
				options.Events = new JwtBearerEvents
				{
					OnTokenValidated = async context =>
					{
						// 1. Lấy DbContext từ DI
						var dbContext = context.HttpContext.RequestServices.GetRequiredService<CannabisAccessoriesDBContext>();
						// 2. Lấy UserId từ Claims (NameIdentifier)
						var userIdClaim = context.Principal?.FindFirst(JwtRegisteredClaimNames.Sub);

						if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
						{
							context.Fail("Token không hợp lệ hoặc thiếu định danh.");
							return;
						}
						// 3. Kiểm tra trạng thái thực tế trong DB
						var userStatus = await dbContext.Users
							.AsNoTracking()
							.Where(u => u.UserId == userId)
							.Select(u => u.Status)
							.FirstOrDefaultAsync();
						// 4. Nếu User không tồn tại hoặc bị khóa, chặn ngay lập tức
						if (userStatus == default || userStatus == EUserStatus.Inactive)
						{
							context.Fail("Tài khoản đã bị khóa hoặc không tồn tại.");
						}
					},
					OnChallenge = async context =>
					{
						context.HandleResponse();
						// Tùy chỉnh trả về ApiResponse khi bị chặn (401)
						if (!context.Response.HasStarted)
						{
							context.Response.StatusCode = StatusCodes.Status401Unauthorized;
							context.Response.ContentType = "application/json";
							string message = "Yêu cầu xác thực để truy cập tài nguyên này.";

							// Nếu có lỗi từ OnTokenValidated gán sang
							if (!string.IsNullOrEmpty(context.ErrorDescription))
							{
								message = context.ErrorDescription;
							}
							else if (!string.IsNullOrEmpty(context.AuthenticateFailure?.Message))
							{
								message = "Token không hợp lệ hoặc đã hết hạn.";
							}
							var result = ApiResponse<object>.Fail(message);
							await context.Response.WriteAsJsonAsync(result);
						}
					},
					OnForbidden = async context =>
					{
						// Xử lý lỗi 403 - Token OK nhưng Role không đủ
						if (!context.Response.HasStarted)
						{
							context.Response.StatusCode = StatusCodes.Status403Forbidden;
							context.Response.ContentType = "application/json";

							var result = ApiResponse<object>.Fail("Bạn không có quyền thực hiện hành động này.");
							await context.Response.WriteAsJsonAsync(result);
						}
					}
				};
			});
			return services;
		}
	}
}
