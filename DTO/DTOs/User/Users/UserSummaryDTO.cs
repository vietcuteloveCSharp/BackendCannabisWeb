namespace DTO.DTOs.User.Users
{
	public class UserSummaryDTO
	{
		public int UserId { get; set; }
		public string Username { get; set; } = default!;
		public string RoleName { get; set; } = default!;
		public string Email { get; set; } = default!;
	}
}
