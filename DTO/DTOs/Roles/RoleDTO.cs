namespace DTO.DTOs.Roles
{
	public class RoleDTO 
	{
		public int RoleId { get; set; }
		public ERoleName RoleName { get; set; }
		public string? Description { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public DateTime? UpdatedAt { get; set; }
		public bool IsDeleted { get; set; } = false;
		public DateTime? DeletedAt { get; set; }
	}
}
