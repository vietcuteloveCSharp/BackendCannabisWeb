namespace DAL.Entities
{
	public class RefreshToken
	{
		public int Id { get; set; }
		public string RefreshTokenValue { get; set; } = default!;
		public DateTime ExpiresAt { get; set; }
		public bool IsRevoked { get; set; }
		public int UserId { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public virtual User User { get; set; } = default!;
	}
}
