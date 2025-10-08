namespace DTO.DTOs.Roles
{
	public class CreateRoleDTO
	{
		[Required(ErrorMessage = "Role name is required.")]
		public ERoleName RoleName { get; set; }
		public string? Description { get; set; }
	}
}
