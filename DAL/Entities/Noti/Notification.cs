using DAL.Entities.Inherited;

namespace DAL.Entities.Noti
{
	public class Notification :BaseEntity,ISoftDelete
	{
		public int Id { get; set; }
		public string Title { get; set; } = default!;

		public string? Message { get; set; }

		public bool IsDeleted { get; set; } = false;
		public DateTime? DeletedAt { get; set; }
		public int? DeletedBy { get; set; }

		public ICollection<NotificationLog> notificationLogs { get; set;} = new List<NotificationLog>();
	}
}
