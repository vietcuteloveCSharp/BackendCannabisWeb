
namespace Service.Implementations.Shared
{
	public class FileService : IFileService
	{
		private readonly string _basePath;
		public FileService(string basePath)
		{
			this._basePath  = basePath;
		}
		public void DeleteFile(string fileUrl)
		{
			
		
			if (string.IsNullOrEmpty(fileUrl)) return;

			try
			{
				// 1. Chuyển URL ảo thành đường dẫn vật lý
				// fileUrl: "/uploads/Spectrum/abc.jpg"
				// _basePath: "D:\Webphukiencannabis\CannabisServer\Uploads"

				// Cần loại bỏ tiền tố "/uploads" để lấy phần thân (Spectrum/abc.jpg)
				var relativePath = fileUrl.Replace("/uploads/", "").Replace("/", Path.DirectorySeparatorChar.ToString());

				var fullPath = Path.Combine(_basePath, relativePath);

				// 2. Kiểm tra xem file có tồn tại thật không rồi mới xóa
				if (File.Exists(fullPath))
				{
					File.Delete(fullPath);
					
				}
			}
			catch (Exception ex)
			{
				// Log lỗi nếu không xóa được (có thể do file đang bị process khác mở)
				Console.WriteLine($"Lỗi khi xóa file: {ex.Message}");
			}
		}
		//upload ảnh 
		public async Task<string> UploadFileAsync(IFormFile file, string folderName)
		{
			if (file == null || file.Length == 0) throw new ArgumentException("File is empty or not selected.");

			// _basePath bây giờ chính là "D:\Projects\...\Uploads" hoặc "E:\..."
			var uploadsFolder = Path.Combine(_basePath, folderName);

			if (!Directory.Exists(uploadsFolder))
				Directory.CreateDirectory(uploadsFolder);

			var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
			var filePath = Path.Combine(uploadsFolder, fileName);

			using (var stream = new FileStream(filePath, FileMode.Create))
			{
				await file.CopyToAsync(stream);
			}

			return $"/uploads/{folderName}/{fileName}";
		}
	}
}
