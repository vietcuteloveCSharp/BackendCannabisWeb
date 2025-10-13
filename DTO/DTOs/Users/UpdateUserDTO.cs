namespace DTO.DTOs.Users
{
	public class UpdateUserDTO
	{
		public string Password { get; set; } = string.Empty;
		public string? Name { get; set; }
		public string Email { get; set; } = string.Empty;
		public string PhoneNumber { get; set; } = string.Empty;
		public EUserStatus Status { get; set; } = EUserStatus.Active;
		public int RoleId { get; set; }
	}
}
