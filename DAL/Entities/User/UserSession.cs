namespace DAL.Entities.User
{
	public class UserSession : BaseEntity,ISoftDelete
	{
		public int Id { get; set; } // khóa chính

		public int UserId { get; set; } // FK User

		public string AccessToken { get; set; } = default!; // access token

		public DateTime ExpiredAt { get; set; } // hết hạn

		public string? Device { get; set; } // thiết bị
		public string? IpAddress { get; set; } // IP
		public bool IsDeleted { get ; set ; }
		public DateTime? DeletedAt { get ; set ; }
		public int? DeletedBy { get; set; }


		// Navigation
		public User User { get; set; } = default!;
	}
}
