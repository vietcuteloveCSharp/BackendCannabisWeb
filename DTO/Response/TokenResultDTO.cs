using DTO.DTOs.User.Users;

namespace DTO.Response
{
	public class TokenResultDTO
	{
		public string AccessToken { get; set; } = default!;
		public string RefreshToken { get; set; } = default!;
		public int ExpiresIn { get; set; } // số giây còn lại
		public UserSummaryDTO User { get; set; } = default!;
	}
}
