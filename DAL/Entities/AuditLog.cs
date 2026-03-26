namespace DAL.Entities
{
    
    public class AuditLog
    {
        [Key]
        public int Id {  get; set; }
		public string TableName { get; set; } = string.Empty;
		public string RecordId { get; set; } = string.Empty; // PK của record bị tác động
		public string Action { get; set; } = string.Empty;   // INSERT / UPDATE / DELETE / SOFT_DELETE
		public string? ColumnName { get; set; }              // cột bị thay đổi (nếu update)
		public string? OldValue { get; set; }
		public string? NewValue { get; set; }
		public int? RoleId { get; set; }
		public string? RoleName { get; set; } 
		public int? UserId {  get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public virtual User? User { get; set; }
		public virtual Role? Role { get; set; }
    }
}
