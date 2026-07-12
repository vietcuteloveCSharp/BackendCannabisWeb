namespace Shared.DTOs.DTO.Shop
{
	/// <summary>
	/// DTO chứa bộ đôi mã Token và thông tin rút gọn của khách hàng sau khi xác thực thành công
	/// </summary>
	public class CustomerTokenResponse
	{
		public string AccessToken { get; set; } = string.Empty;
		public string RefreshToken { get; set; } = string.Empty;
		public int ExpiresInSeconds { get; set; }
		public CustomerSummaryDTO Customer { get; set; } = default!;
	}

	/// <summary>
	/// Thông tin rút gọn của Khách hàng phục vụ hiển thị trên Header/Profile của giao diện Web
	/// </summary>
	public class CustomerSummaryDTO
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Username { get; set; }
		public string? Email { get; set; }
		public string? AvatarUrl { get; set; }
	}

	/// <summary>
	/// DTO chi tiết cấu trúc hồ sơ Khách hàng (Dùng khi cần trả về thông tin CRUD đầy đủ)
	/// </summary>
	public class CustomerDTO
	{
		public int Id { get; set; }
		public string? Username { get; set; }
		public string? Name { get; set; }
		public string? Email { get; set; }
		public string? PhoneNumber { get; set; }
		public string? AvatarUrl { get; set; }
		public bool IsActive { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? LastLoginAt { get; set; }
	}
}

