namespace DAL.Entities.Shop
{
	public class Customer :BaseEntity, ISoftDelete
	{
		[Key]
		public int Id { get; set; }
		public string? AvatarUrl { get; set; }
		public string? Username { get; set; }
		public string? PasswordHash { get; set; }
		public string? Name { get; set; }
		public string? Email { get; set; }
		public string PhoneNumber { get; set; } = string.Empty;
		public bool EmailConfirmed { get; set; }
		public bool PhoneConfirmed { get; set; }
		public DateTime? LastLoginAt { get; set; }

		// ⚡ THAY THẾ: Không dùng StatusId nữa, dùng trường nhanh này để check Block/Active
		public bool IsActive { get; set; } = true;

		public bool IsDeleted { get; set; }
		public DateTime? DeletedAt { get; set; }
		public int? DeletedBy { get; set; }

		// --- Navigation Properties nghiệp vụ E-commerce mua sắm ---
		public virtual Cart? Cart { get; set; }

		public virtual ICollection<Order> OrdersAsBuyer { get; set; } = new List<Order>();
		public virtual ICollection<CustomerRefreshToken> RefreshTokens { get; set; } = new List<CustomerRefreshToken>();
		public virtual ICollection<CustomerSession> CustomerSessions { get; set; } = new HashSet<CustomerSession>();
		public virtual ICollection<Address>? Addresses { get; set; } = new HashSet<Address>();
		public virtual ICollection<Review>? Reviews { get; set; } = new HashSet<Review>();
		public virtual ICollection<Wishlist> Wishlists { get; set; } = new HashSet<Wishlist>();
		public virtual ICollection<CustomerNotificationLog> NotificationLogs { get; set; } = new HashSet<CustomerNotificationLog>();
	}
}
