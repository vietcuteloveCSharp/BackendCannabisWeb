namespace DAL.Entities.Internal
{
	public class StaffSession : BaseEntity, ISoftDelete
	{
		[Key]
		public int Id { get; set; }
		public int StaffId { get; set; } // FK trỏ tới Staff
		public string SessionToken { get; set; } = string.Empty;
		public DateTime LoginAt { get; set; } = DateTime.UtcNow;
		public DateTime ExpiresAt { get; set; }
		public string? UserAgent { get; set; }
		public string? IpAddress { get; set; }

		public bool IsDeleted { get; set; }
		public DateTime? DeletedAt { get; set; }
		public int? DeletedBy { get; set; }

		// Navigation
		public virtual Staff Staff { get; set; } = default!;
		public virtual ICollection<StaffRefreshToken> RefreshTokens { get; set; } = new List<StaffRefreshToken>();
	}
}
