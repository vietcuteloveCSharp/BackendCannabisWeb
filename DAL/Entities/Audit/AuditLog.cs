namespace DAL.Entities.Audit
{ 
    public class AuditLog
    {
		public int Id { get; set; }
		public int? UserId { get; set; } // ai thực hiện hành động
		public string Action { get; set; } = default!; // ví dụ: "Create", "Update", "Delete"
		public string TableName { get; set; } = default!; // tên bảng/entity
		public DateTime ActionTime { get; set; } = DateTime.UtcNow;
		public string? KeyValues { get; set; } // id hoặc key entity bị thay đổi
		public string? OldValues { get; set; } // dữ liệu cũ
		public string? NewValues { get; set; } // dữ liệu mới
		public string? ChangedColumns { get; set; } // tên cột bị thay đổi

		// Navigation
		public virtual ICollection<EntityChange> EntityChanges { get; set; } = new HashSet<EntityChange>();
	}
}
