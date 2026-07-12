namespace Cannabis.Server.DependencyInjection
{
	public static class ApiVersionExtensions
	{   // cấu hình version
		public static IServiceCollection AddApiVersion(this IServiceCollection services)
		{
			var versioningBuilder = services.AddApiVersioning(options =>
			{
				options.ReportApiVersions = true;
				options.AssumeDefaultVersionWhenUnspecified = true;
				options.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0);
				options.ApiVersionReader = new Asp.Versioning.UrlSegmentApiVersionReader();
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
