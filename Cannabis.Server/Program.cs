



namespace Cannabis.Server
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// ✅ Xác định môi trường
			var env = builder.Environment.EnvironmentName;
			
			builder.Configuration
				.SetBasePath(builder.Environment.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true)
				.AddEnvironmentVariables();

			// Add services to the container.
			builder.Services.AddControllers(options =>
			{
				options.Filters.Add<ValidateModelAttribute>();
				options.ReturnHttpNotAcceptable = true;
				options.OutputFormatters.RemoveType<StringOutputFormatter>();
			})
				.AddJsonOptions(otps =>
				{
					otps.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
				});


			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(c =>
			{
				c.MapType<ApiResponse<object>>(() => new OpenApiSchema { Type = "object" });
				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Name = "Authorization",
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer",
					BearerFormat = "JWT",
					In = ParameterLocation.Header,
					Description = "Enter 'Bearer' [space] and then your token."
				});
				c.AddSecurityRequirement(new OpenApiSecurityRequirement {
					{
						new OpenApiSecurityScheme {
							Reference = new OpenApiReference {
							Type = ReferenceType.SecurityScheme,
							Id = "Bearer"
							}
						},
						new string[] {}
					}
				});
			});
			//1.cấu hình db web
			builder.Services.AddDbContext<CannabisAccessoriesDBContext>(options =>
			{
				var connectionString = builder.Configuration.GetConnectionString("CannabisAccessoriesDB");
				// Chỉ gọi UseSqlServer nếu connectionString có giá trị
				if (!string.IsNullOrEmpty(connectionString))
				{
					options.UseSqlServer(connectionString,
						sqlOptions => sqlOptions.EnableRetryOnFailure(
					  maxRetryCount: 5, // cố lần retry tối đa
					  maxRetryDelay: TimeSpan.FromSeconds(10), //delay giữa các retry
					  errorNumbersToAdd: null));// nếu muốn thêm lỗi sql cụ thể
				}
				options.EnableSensitiveDataLogging()
						.LogTo(Console.WriteLine, LogLevel.Information);
			});
			// 2. THÊM MỚI: Cấu hình DB Audit Log riêng biệt
			builder.Services.AddDbContext<AuditDbContext>(options =>
			{
				var auditConnectionString = builder.Configuration.GetConnectionString("CannabisAccessoriesAuditDB");
				if (!string.IsNullOrEmpty(auditConnectionString))
				{
					options.UseSqlServer(auditConnectionString, sqlOptions => sqlOptions.EnableRetryOnFailure(
						maxRetryCount: 5,
						maxRetryDelay: TimeSpan.FromSeconds(10),
						errorNumbersToAdd: null));
				}
			});
			builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
			builder.Services.Configure<RouteOptions>(options => { options.LowercaseUrls = true; });

			//đăng kí dịch vụ auto mapper, repository, service, mailkit, redis
			builder.Services.AddApplicationAutoMapper();
			builder.Services.AddApplicationRepositories();
			builder.Services.AddApplicationServices();
			builder.Services.AddInfrastructureServices(builder.Configuration);
			builder.Services.AddFileConfiguration(builder.Environment);
			builder.Services.AddHttpContextAccessor();
			//cấu hình cors
			builder.Services.AddCors(options =>
			{
				options.AddPolicy("AllowAngularApp", policy =>
				{
					policy.WithOrigins("http://localhost:4200")
						  .AllowAnyMethod()
						  .AllowAnyHeader()
						  .AllowCredentials();
				});
			});
			// Console.WriteLine($"JWT KEY = {builder.Configuration["Jwt:Key"]}");
			//Cấu hình JWT
			builder.Services.AddJwtAuth(builder.Configuration);
			// cấu hình version
			builder.Services.AddApiVersion();
			var app = builder.Build();

			await app.SeedDatabaseAsync();
			// 1. Xử lý lỗi toàn cục - Phải nằm trên cùng để bắt mọi lỗi của các Middleware sau
			app.UseMiddleware<GlobalExceptionMiddleware>();
			// 2. HTTPS Redirection
			app.UseHttpsRedirection();
			app.UseStaticFiles(); // Cho wwwroot
			// 3. Cấu hình File tĩnh (Phải đặt TRƯỚC Routing để truy cập ảnh nhanh nhất)
			
			var uploadPath = Path.Combine(app.Environment.ContentRootPath, "uploads");
			if (!Directory.Exists(uploadPath)) Directory.CreateDirectory(uploadPath);

			app.UseStaticFiles(new StaticFileOptions
			{
				FileProvider = new PhysicalFileProvider(uploadPath),
				RequestPath = "/api/uploads"
			});
			// 2. Swagger - Chỉ dùng trong môi trường Phát triển
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			// 4. Routing - Định tuyến (Bắt buộc phải đứng trước CORS và Auth)
			app.UseRouting();

			// 5. CORS - Chỉ dùng DUY NHẤT một dòng này. 
			// Đừng dùng cái 'if Development' kèm 'AllowAnyOrigin' ở đây nữa vì nó sẽ gây lỗi Wildcard '*'
			app.UseCors("AllowAngularApp");

			// 6. Xác thực & Phân quyền
			app.UseAuthentication();
			app.UseAuthorization();
			app.UseMiddleware<AdminAuthorizeMiddleware>();

			// 7. Map Controllers
			app.MapControllers();

			app.Run();
		}
	}
}
