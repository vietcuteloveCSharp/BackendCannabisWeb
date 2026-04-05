using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace Cannabis.Server.DependencyInjection
{
	public static class JwtExtensions
	{
		public static IServiceCollection AddJwtAuth(this IServiceCollection services, IConfiguration config)
		{  
			

			var jwtSection = config.GetSection("Jwt");
			// Lấy giá trị ngay bây giờ từ config (để dùng khi cấu hình TokenValidationParameters)
			var jwtSettings = jwtSection.Get<JwtSettings>()
				?? throw new InvalidOperationException("Jwt section is missing in configuration.");


			// 2. Kiểm tra Null một cách an toàn
			if (jwtSettings == null || string.IsNullOrWhiteSpace(jwtSettings.Key))
			{
				throw new InvalidOperationException("Cấu hình JWT (Key) bị thiếu trong appsettings.json.");
			}
			var key = Encoding.UTF8.GetBytes(jwtSettings.Key);

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
				

				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = jwtSettings.Issuer,
					ValidAudience = jwtSettings.Audience,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					NameClaimType = ClaimTypes.Name,
					RoleClaimType = ClaimTypes.Role,
					ClockSkew = TimeSpan.Zero // loại bỏ độ trễ thời gian
				};
				// cấu hình event cho product
				options.Events = new JwtBearerEvents
				{
					OnAuthenticationFailed = context =>
					{
						// Đặt Breakpoint tại dòng này
						var error = context.Exception.Message;
						Console.WriteLine($"Token lỏ vì: {error}");

						// Nếu lỗi do hết hạn, context.Exception sẽ là SecurityTokenExpiredException
						return Task.CompletedTask;
					},
					OnTokenValidated = async context =>
					{
						// 1. Lấy DbContext từ DI
						var dbContext = context.HttpContext.RequestServices.GetRequiredService<CannabisAccessoriesDBContext>();
						// 2. Lấy UserId từ Claims (NameIdentifier)
						var userIdClaim = context.Principal?.FindFirst(ClaimTypes.NameIdentifier);
						
						if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
						{
							context.Fail("Unauthorized: Missing User ID.");
							return;
						}
						// 3. Kiểm tra trạng thái thực tế trong DB
						var user = await dbContext.Users
							.AsNoTracking()
							.Where(u => u.Id == userId)
							.Select(u => new { u.Status })
							.FirstOrDefaultAsync();
						// 4. Nếu User không tồn tại hoặc bị khóa, chặn ngay lập tức
						if (user == null)
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

							var failureReason = context.AuthenticateFailure?.Message;
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
