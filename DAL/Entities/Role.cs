namespace DAL.Entities
{
    [Table("Roles",Schema = "Users")]
    public class Role :BaseEntity
    {
        [Key]
        public int RoleId { get; set; }
        [Required(ErrorMessage ="Role name is required.")]
        public ERoleName RoleName { get; set; }
        public string? Description { get; set; }

        public ICollection<User> Users { get; set; } = new HashSet<User>();
        public ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();
	}
}
