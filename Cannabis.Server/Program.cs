using Cannabis.Server.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace Cannabis.Server
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			var jwtKey = builder.Configuration["Jwt:Key"]
			?? throw new InvalidOperationException("JWT Key is not configured.");
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

			builder.Services.Configure<JwtSettings>
				(builder.Configuration.GetSection("Jwt"));

			builder.Services.AddAutoMapper(typeof(MapperDTO_Entity));
			builder.Services.AddApplicationRepositories();
			builder.Services.AddApplicationServices();
			builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
				options =>
				{
					options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
					{
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						ValidIssuer = builder.Configuration["Jwt:Issuer"],
						ValidAudience = builder.Configuration["Jwt:Audience"],

						IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtKey))
					};
				});
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
			app.UseMiddleware<GlobalExceptionMiddleware>();
			app.UseMiddleware<JwtMiddleware>();
			app.UseAuthentication();
			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
