using DTO.DTOs.User.Users;

namespace DTO.TokenDTOs
{
	public class TokenDTO
	{
		public string AccessToken { get; set; } = default!;
		public string RefreshToken { get; set; } = default!;
		public TimeSpan ExpiresIn { get; set; }
		public UserSummaryDTO? User { get; set; } = default!;
	}
}
