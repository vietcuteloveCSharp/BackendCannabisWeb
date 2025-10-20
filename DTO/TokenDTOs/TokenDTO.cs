using DTO.DTOs.User.Users;

namespace DTO.TokenDTOs
{
	public class TokenDTO
	{
		public string AccessToken { get; set; } = default!;
		public string RefreshToken { get; set; } = default!;
		public DateTime Expiration { get; set; }
		public UserSummaryDTO? User { get; set; } = default!;
	}
}
