namespace DTO.DTOs.Roles
{
	public class RoleCreateDTO
	{
		[Required(ErrorMessage = "Role name is required.")]
		public string? Description { get; set; }
	}
}
