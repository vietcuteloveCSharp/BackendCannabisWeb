using DAL.Entities.Inherited;

namespace DAL.Entities.Noti
{
	public class NotificationLog :BaseEntity
	{

		public int Id { get; set; }
		public int NotificationId { get; set; } // FK Notification
		public int UserId { get; set; } // nhận thông báo

		public bool IsRead { get; set; } = false;

		// Navigation
		public Notification Notification { get; set; } = default!;
		public User.User User { get; set; } = default!;
	}
}
