namespace DAL.Entities
{
	[Table("RefreshTokens", Schema = "Users")]
	public class RefreshToken
	{
		public int Id { get; set; }
		public string RefreshTokenValue { get; set; } = default!;
		public DateTime ExpiresAt { get; set; }
		public bool IsRevoked { get; set; }
		public int UserId { get; set; }
		public virtual User? User { get; set; }
	}
}
