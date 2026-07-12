namespace DAL.Entities.Internal
{
	public class StaffRefreshToken : BaseEntity, ISoftDelete
	{
		[Key]
        public int Id { get; set; }
		public int StaffId { get; set; } // FK trỏ tới Staff
		public int StaffSessionId { get; set; } // FK trỏ tới StaffSession
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
		public virtual Staff Staff { get; set; } = default!;
		public virtual StaffSession StaffSession { get; set; } = null!;
	}
}
