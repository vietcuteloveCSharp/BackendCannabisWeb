using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IServices.Shared
{
	public interface IFileService
	{
		// Trả về chuỗi URL sau khi lưu thành công
		Task<string> UploadFileAsync(IFormFile file, string folderName);
		void DeleteFile(string fileUrl);
	}
}
