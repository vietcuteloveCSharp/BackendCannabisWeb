using System.Text.Json.Serialization;

namespace DTO.DTOs.User.Users
{
	public class UserDTO
	{
		public int Id { get; set; }
		public string Username { get; set; } = string.Empty;

		[JsonIgnore] // Không nên trả Password về client dù là rỗng
		public string Password { get; set; } = string.Empty;
		public string? Name { get; set; }
		public string Email { get; set; } = string.Empty;
		public string PhoneNumber { get; set; } = string.Empty;
		public DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }

		// 🚩 Đổi sang string để hiển thị "Active"/"Blocked" thay vì số
		public string Status { get; set; } = string.Empty;

		public int RoleId { get; set; }

		// 🚩 Bổ sung trường này để AutoMapper map từ Role.RoleName sang
		public string RoleName { get; set; } = string.Empty;
	}
}
