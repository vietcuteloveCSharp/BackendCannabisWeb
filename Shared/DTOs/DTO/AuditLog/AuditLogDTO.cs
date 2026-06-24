namespace Shared.DTOs.DTO.AuditLog
{
	public class AuditLogDTO
	{
		public int? UserId { get; set; }
		public string Action { get; set; } = string.Empty;
		public string TableName { get; set; } = string.Empty;
		public DateTime ActionTime { get; set; }
		public string? KeyValues { get; set; }
		public string? OldValues { get; set; }
		public string? NewValues { get; set; }
		public string? ChangedColumns { get; set; }
	}
}
