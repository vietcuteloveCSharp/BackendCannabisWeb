namespace Shared.DTOs.DTO.RefreshTokens
{
	public class RefreshTokenCreateDTO
	{
		public int UserId { get; set; }
		public string RefreshTokenValue { get; set; } = default!;
		public DateTime ExpiresAt { get; set; }
	}
}
