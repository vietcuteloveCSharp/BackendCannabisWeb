

namespace DAL.Entities.Shop
{
	public class CustomerSession : BaseEntity, ISoftDelete
	{
		[Key]
		public int Id { get; set; }
		public int CustomerId { get; set; } // FK trỏ tới Customer
		public string SessionToken { get; set; } = string.Empty;
		public DateTime LoginAt { get; set; } = DateTime.UtcNow;
		public DateTime ExpiresAt { get; set; }
		public string? UserAgent { get; set; }
		public string? IpAddress { get; set; }

		public bool IsDeleted { get; set; }
		public DateTime? DeletedAt { get; set; }
		public int? DeletedBy { get; set; }

		// Navigation
		public virtual Customer Customer { get; set; } = default!;
		public virtual ICollection<CustomerRefreshToken> RefreshTokens { get; set; } = new List<CustomerRefreshToken>();
	}
}
