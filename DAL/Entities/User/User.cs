
using DAL.Entities.Inherited;

namespace DAL.Entities.User
{
    public class User :BaseEntity ,ISoftDelete
	{
		[Key]
		public int Id { get; set; }
		public string? AvatarUrl { get; set; }
		public string? Username { get; set; }
        public string? PasswordHash { get; set; }
		public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
		public bool EmailConfirmed { get; set; } // đã xác thực email chưa
        public bool PhoneConfirmed { get; set; } // đã xác thực sdt chưa
		public DateTime? LastLoginAt { get; set; } // lần đăng nhập gần nhất
		public int StatusId { get; set; }  // FK UserStatus
        public int RoleId { get; set; } // RoleId
		public bool IsDeleted { get; set; } // xóa mềm
		public DateTime? DeletedAt { get; set; } // thời điểm xóa
		public int? DeletedBy { get; set; } // ai xóa
		public virtual Role Role { get; set; } = default!;
		public virtual UserStatus Status { get; set; } = default!;
		public virtual DAL.Entities.Cart.Cart? Cart { get; set; } 
		// 1. Đơn hàng người dùng mua
		public virtual ICollection<Order.Order> OrdersAsBuyer { get; set; } = new List<Order.Order>();

		// 2. Đơn hàng người dùng bán
		public virtual  ICollection<Order.Order> OrdersAsStaff { get; set; } = new List<Order.Order>();
		public virtual ICollection<UserRefreshToken> RefreshTokens { get; set; } =new List<UserRefreshToken>();
        public virtual ICollection<Address> ? Addresses { get; set; } = new HashSet<Address>();
		public virtual ICollection<Review.Review>? Reviews { get; set; } = new HashSet<Review.Review>();
		public virtual ICollection<UserSession> Sessions { get; set; } = new HashSet<UserSession>();
		public virtual ICollection<Wishlist> Wishlists { get; set; } =new HashSet<Wishlist>();
		public virtual ICollection<NotificationLog> NotificationLogs { get; set; } = new HashSet<NotificationLog>();
		public virtual ICollection<AuditLog> AuditLogs { get; set; }=new HashSet<AuditLog>();
	}
}
