namespace Shared.DTOs.DTO.Roles
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
	public class RoleCreateDTO
	{
		[Required(ErrorMessage = "Role name is required.")]
		public string RoleName { get; set; } = string.Empty;
		public string? Description { get; set; }
	}
	public class RoleUpdateDTO
	{
		public string? Description { get; set; }
	}
}
