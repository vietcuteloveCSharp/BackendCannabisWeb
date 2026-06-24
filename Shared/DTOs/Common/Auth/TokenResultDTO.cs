
using Shared.DTOs.DTO.User.Users;

namespace Shared.Common.Auth
{	//dùng cho api
	public class TokenResultDTO
	{
		public string AccessToken { get; set; } = default!;
		public DateTime ExpiresAt { get; set; }
		public UserSummaryDTO User { get; set; } = default!;
	}
}
