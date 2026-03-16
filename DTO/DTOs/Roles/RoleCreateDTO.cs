namespace DTO.DTOs.Roles
{
	public class RoleCreateDTO
	{
		[Required(ErrorMessage = "Role name is required.")]
		public ERoleName RoleName { get; set; }
		public string? Description { get; set; }
	}
}
