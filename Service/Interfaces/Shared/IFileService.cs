using Microsoft.AspNetCore.Http;
namespace Service.Interfaces.Shared
{
	public interface IFileService
	{
		// Trả về chuỗi URL sau khi lưu thành công
		Task<string> UploadFileAsync(IFormFile file, string folderName);
		void DeleteFile(string fileUrl);
	}
}
