namespace Shared.DTOs.DTO.RefreshTokens
{
	public class RefreshTokenDTO
	{
		public string RefreshTokenValue { get; set; } = default!;
		public DateTime ExpiresAt { get; set; }
		public bool IsRevoked { get; set; }
		public int UserId { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	}
}
