namespace DTO.DTOs.User.Users
{
	public class UpdateUserDTO
	{
		public string Password { get; set; } = string.Empty;
		public string? Name { get; set; }
		public string Email { get; set; } = string.Empty;
		public string PhoneNumber { get; set; } = string.Empty;
	}
}
