
namespace DAL.Entities.Noti
{
	public class CustomerNotificationLog : BaseEntity
	{
		public int Id { get; set; }
		public int NotificationId { get; set; } // FK trỏ tới bảng Notification
		public int CustomerId { get; set; } // FK trỏ tới bảng Customer nhận thông báo

		public bool IsRead { get; set; } = false;

		// --- Navigation Properties ---
		public virtual Notification Notification { get; set; } = default!;
		public virtual Customer Customer { get; set; } = default!;
	}
}
