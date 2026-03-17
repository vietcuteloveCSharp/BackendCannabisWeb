namespace DAL.Entities
{
    
    public class AuditLog
    {
        [Key]
        public int AuditLogId {  get; set; }
		[Required, StringLength(150)]
		public string TableName { get; set; } = string.Empty;
		[Required, StringLength(100)]
		public string RecordId { get; set; } = string.Empty; // PK của record bị tác động
		[Required]
		public EActionLog Action { get; set; }   // INSERT / UPDATE / DELETE / SOFT_DELETE
		[StringLength(150)]
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
