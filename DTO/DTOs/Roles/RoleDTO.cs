namespace DTO.DTOs.Roles
{
	public class RoleDTO 
	{
		public int Id { get; set; }
		public string RoleName { get; set; } = string.Empty;
		public string? Description { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public DateTime? UpdatedAt { get; set; }
		public bool IsDeleted { get; set; } = false;
		public DateTime? DeletedAt { get; set; }
	}
}
