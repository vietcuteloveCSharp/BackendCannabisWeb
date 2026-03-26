using Service.IServices.Shared;
using Service.Services.Shared;

namespace Cannabis.Server.DependencyInjection
{
	public static class FileExtension
	{
		public static IServiceCollection AddFileConfiguration(this IServiceCollection services, IWebHostEnvironment env)
		{
			// 1. Xác định đường dẫn thư mục Uploads ở ổ D (trong project)
			string uploadFolder = Path.Combine(env.ContentRootPath, "Uploads");

			// 2. Kiểm tra và tạo thư mục nếu chưa có
			if (!Directory.Exists(uploadFolder))
			{
				Directory.CreateDirectory(uploadFolder);
			}
			// Lưu ý: FileService của bạn phải có Constructor nhận string basePath
			services.AddScoped<IFileService>(provider => new FileService(uploadFolder));
			return services;
		}
	}
}
