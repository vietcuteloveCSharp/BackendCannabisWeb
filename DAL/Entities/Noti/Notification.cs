using Shared.Common.Inherited;

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

		public virtual ICollection<CustomerNotificationLog> CustomerNotificationLogs { get; set; } = new List<CustomerNotificationLog>();
		public virtual ICollection<StaffNotificationLog> StaffNotificationLogs { get; set; } = new List<StaffNotificationLog>();
	}
}
