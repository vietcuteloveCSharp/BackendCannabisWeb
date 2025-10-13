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
			// đăng ký JwtBearer và lấy options từ DI
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
					});
			return services;
		}
	}
}
