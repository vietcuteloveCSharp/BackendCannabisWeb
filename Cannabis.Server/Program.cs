
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
			builder.Services.AddControllers()
				.AddJsonOptions(otps =>
				{
					otps.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
				});

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddDbContext<CannabisAccessorriesDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("CannabisAccessorriesDB")));

			//đăng kí dịch vụ auto mapper, repository, service,mailkit,redis
			builder.Services.AddAutoMapper(typeof(MapperDTO_Entity));
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
			// Cấu hình JWT
			builder.Services.AddJwtAuth(builder.Configuration);
			// cấu hình version
			builder.Services.AddApiVersioning(opt =>
			{
				opt.ReportApiVersions = true;
				opt.AssumeDefaultVersionWhenUnspecified = true;
				opt.DefaultApiVersion = new ApiVersion(1, 0);
				opt.ApiVersionReader = new UrlSegmentApiVersionReader();

			});
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
			app.UseMiddleware<JwtMiddleware>();
			app.UseAuthentication();
			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
