namespace DTO.DTOs.User.Users
{
	public class UserDTO
	{
		public int UserId { get; set; }
		public string Username { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
		public string? Name { get; set; }
		public string Email { get; set; } = string.Empty;
		public string PhoneNumber { get; set; } = string.Empty;
		public DateTime CreatedAt { get; set; } 
		public DateTime? UpdatedAt { get; set; }
		[Column(TypeName = "nvarchar(20)")]
		public EUserStatus Status { get; set; } = EUserStatus.Active;
		public int RoleId { get; set; }
	}
}
