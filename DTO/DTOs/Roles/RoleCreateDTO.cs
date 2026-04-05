namespace DTO.DTOs.Roles
{
	public class RoleCreateDTO
	{
		[Required(ErrorMessage = "Role name is required.")]
		public string RoleName { get; set; } = string.Empty;
		public string? Description { get; set; }
	}
}
