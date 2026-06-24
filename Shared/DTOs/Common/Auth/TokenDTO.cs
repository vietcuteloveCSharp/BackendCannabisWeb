
using Shared.DTOs.DTO.User.Users;

namespace Shared.Common.Auth
{	//dùng nội bộ
	public class TokenDTO
	{
		public string AccessToken { get; set; } = default!;
		public string RefreshToken { get; set; } = default!;
		public int ExpiresInSeconds { get; set; } // Sửa từ DateTime thành int
		public UserSummaryDTO User { get; set; } = default!;
	}
}
