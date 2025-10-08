namespace DTO.DTOs.Users
{
	public class UserSummaryDTO
	{
		public int UserId { get; set; }
		public string Username { get; set; } = default!;
		public string RoleName { get; set; } = default!;
	}
}
