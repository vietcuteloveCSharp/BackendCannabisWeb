using Shared.Common.Inherited;

namespace DAL.Entities.Audit
{
	public class EntityChange : BaseEntity
	{
		[Key]
		public int Id { get; set; }

		public int AuditLogId { get; set; } // FK AuditLog

		public string PropertyName { get; set; } = default!; // tên cột bị thay đổi
		public string? OldValue { get; set; }
		public string? NewValue { get; set; }

		// Navigation
		public virtual AuditLog AuditLog { get; set; } = default!;
	}
}
