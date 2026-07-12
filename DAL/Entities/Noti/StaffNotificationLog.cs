namespace DAL.Entities.Noti
{
	public class StaffNotificationLog : BaseEntity
	{
		public int Id { get; set; }
		public int NotificationId { get; set; } // FK trỏ tới bảng Notification
		public int StaffId { get; set; } // FK trỏ tới bảng Staff nhận thông báo

		public bool IsRead { get; set; } = false;

		// --- Navigation Properties ---
		public virtual Notification Notification { get; set; } = default!;
		public virtual Staff Staff { get; set; } = default!;
	}
}
