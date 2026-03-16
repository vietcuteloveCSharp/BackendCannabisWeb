using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOs.Shared
{
	public class LogoutRequestDTO
	{
		[Required(ErrorMessage = "Refresh Token là bắt buộc để đăng xuất.")]
		public string RefreshToken { get; set; } = string.Empty;
	}
}
