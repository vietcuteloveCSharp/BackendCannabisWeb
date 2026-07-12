

namespace DAL.Entities.Shop
{
	public class CustomerRefreshToken : BaseEntity, ISoftDelete
	{
		[Key]
		public int Id { get; set; }
		public int CustomerId { get; set; } // FK trỏ tới Customer
		public int CustomerSessionId { get; set; } // FK trỏ tới CustomerSession
		public string TokenHash { get; set; } = default!;
		public DateTime ExpiresAt { get; set; }
		public bool IsUsed { get; set; }
		public bool IsRevoked { get; set; }
		public string? Device { get; set; }
		public string? IpAddress { get; set; }

		public bool IsDeleted { get; set; }
		public DateTime? DeletedAt { get; set; }
		public int? DeletedBy { get; set; }

		// Navigation
		public virtual Customer Customer { get; set; } = default!;
		public virtual CustomerSession CustomerSession { get; set; } = null!;
	}

	
}
