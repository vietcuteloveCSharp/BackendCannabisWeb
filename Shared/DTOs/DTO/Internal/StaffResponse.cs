
namespace Shared.DTOs.DTO.Internal
{
	// Feature: Quản trị thông tin nhân viên (Bản đầy đủ hiển thị danh sách/chi tiết)
	public class StaffDTO
	{
		public int Id { get; set; }
		public string? AvatarUrl { get; set; }
		public string? Username { get; set; }
		public string? Name { get; set; }
		public string? Email { get; set; }
		public string PhoneNumber { get; set; } = string.Empty;
		public string StaffCode { get; set; } = string.Empty;
		public bool EmailConfirmed { get; set; }
		public bool PhoneConfirmed { get; set; }
		public DateTime? LastLoginAt { get; set; }

		// Dữ liệu đã làm phẳng (Flattening) để Client Admin dùng luôn
		public int RoleId { get; set; }
		public string RoleName { get; set; } = string.Empty;

		public int StatusId { get; set; }
		public string StatusName { get; set; } = string.Empty;
	}

	// Feature: Phản hồi thông tin Token kèm thông tin tóm tắt của Staff khi đăng nhập thành công
	public class TokenResponse
	{
		public string AccessToken { get; set; } = string.Empty;
		public string RefreshToken { get; set; } = string.Empty;
		public int ExpiresInSeconds { get; set; }
		public StaffSummaryDTO Staff { get; set; } = default!;
	}

	// DTO tóm tắt hiển thị nhanh góc màn hình sau khi đăng nhập thành công
	public class StaffSummaryDTO
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Username { get; set; }
		public string? AvatarUrl { get; set; }
		public string RoleName { get; set; } = string.Empty;
	}
}
