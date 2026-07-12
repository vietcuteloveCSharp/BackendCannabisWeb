namespace DAL.Entities.Internal
{
	
		public class Staff : BaseEntity, ISoftDelete
		{
			[Key]
			public int Id { get; set; }
			public string? AvatarUrl { get; set; }
			public string? Username { get; set; }
			public string? PasswordHash { get; set; }
			public string? Name { get; set; }
			public string? Email { get; set; }
			public string PhoneNumber { get; set; } = string.Empty;
			public string StaffCode { get; set; } = string.Empty; // Mã nhân viên
			public bool EmailConfirmed { get; set; }
			public bool PhoneConfirmed { get; set; }
			public DateTime? LastLoginAt { get; set; }

			public int StatusId { get; set; }
			public int RoleId { get; set; }

			public bool IsDeleted { get; set; }
			public DateTime? DeletedAt { get; set; }
			public int? DeletedBy { get; set; }

			// --- Navigation Properties ---
			public virtual Role Role { get; set; } = default!;
			public virtual StaffStatus Status { get; set; } = default!;

			public virtual ICollection<Shop.Order> OrdersAsStaff { get; set; } = new List<Shop.Order>();
			public virtual ICollection<StaffRefreshToken> RefreshTokens { get; set; } = new List<StaffRefreshToken>();
			public virtual ICollection<StaffSession> StaffSessions { get; set; } = new HashSet<StaffSession>();
			public virtual ICollection<AuditLog> AuditLogs { get; set; } = new HashSet<AuditLog>();
			public virtual ICollection<StaffNotificationLog> NotificationLogs { get; set; } = new HashSet<StaffNotificationLog>();

	}
}
