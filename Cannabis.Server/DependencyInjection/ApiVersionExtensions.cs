namespace Cannabis.Server.DependencyInjection
{
	public static class ApiVersionExtensions
	{   // cấu hình version
		public static IServiceCollection AddApiVersion(this IServiceCollection services)
		{
			var versioningBuilder = services.AddApiVersioning(otpions =>
			{
				otpions.ReportApiVersions = true;
				otpions.AssumeDefaultVersionWhenUnspecified = true;
				otpions.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0);
				otpions.ApiVersionReader = new Asp.Versioning.UrlSegmentApiVersionReader();
			});
			// ✅ Bây giờ gọi AddApiExplorer từ versioningBuilder sẽ không còn lỗi
			versioningBuilder.AddApiExplorer(options =>
			{
				options.GroupNameFormat = "'v'VVV";
				options.SubstituteApiVersionInUrl = true;
			});
			return services;
		}
	}
}
