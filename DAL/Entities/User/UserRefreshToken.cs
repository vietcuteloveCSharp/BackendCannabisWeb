using DAL.Entities.Inherited;

namespace DAL.Entities.User
{
	public class UserRefreshToken : BaseEntity,ISoftDelete
	{
		public int Id { get; set; }
		public int UserId { get; set; } //FK tới user
		public int UserSessionId { get; set; }
		public string  TokenHash { get; set; } = default!;
		public DateTime ExpiresAt { get; set; }
		public bool IsUsed { get; set; } // đã dùng chưa
		public bool IsRevoked { get; set; }
		public string? Device { get; set; } // thiết bị
		public string? IpAddress { get; set; } // IP
		public bool IsDeleted { get; set; }
		public DateTime? DeletedAt { get; set; }
		public int? DeletedBy { get; set; }
		public virtual User User { get; set; } = default!;
		public virtual UserSession UserSession { get; set; } = null!;

	}
}
