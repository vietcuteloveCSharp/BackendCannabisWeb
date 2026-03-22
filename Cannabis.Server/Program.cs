using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.OpenApi.Models;

namespace Cannabis.Server
{
	public class Program
	{
		public static void Main(string[] args)
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

			//đăng kí dịch vụ auto mapper, repository, service, mailkit, redis
			builder.Services.AddApplicationAutoMapper();
			builder.Services.AddApplicationRepositories();
			builder.Services.AddApplicationServices();
			builder.Services.AddInfrastructureServices(builder.Configuration);
			//cấu hình cors
			builder.Services.AddCors(options =>
			{
				options.AddPolicy("AllowAll", policy =>
				{
					policy.AllowAnyOrigin()
						  .AllowAnyMethod()
						  .AllowAnyHeader();
				});
			});
			Console.WriteLine($"JWT KEY = {builder.Configuration["Jwt:Key"]}");
			//Cấu hình JWT
			builder.Services.AddJwtAuth(builder.Configuration);
			// cấu hình version
			builder.Services.AddApiVersioning(opt =>
			{
				opt.ReportApiVersions = true;
				opt.AssumeDefaultVersionWhenUnspecified = true;
				opt.DefaultApiVersion = new ApiVersion(1, 0);
				opt.ApiVersionReader = new UrlSegmentApiVersionReader();

			});
			//.AddApiExplorer(options =>
			// {
			//	 options.GroupNameFormat = "'v'VVV";
			//	 options.SubstituteApiVersionInUrl = true; // Cái này cực kỳ quan trọng để resolve {version:apiVersion}
			// });
			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}
			app.UseHttpsRedirection();
			app.UseCors("AllowAll");
			app.UseMiddleware<GlobalExceptionMiddleware>();
			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
