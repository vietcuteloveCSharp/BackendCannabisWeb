
namespace Shared.DTOs.DTO.Shop
{
	// Feature: Đăng ký tài khoản
	public class RegisterRequest
	{
		[Required(ErrorMessage = "Tên đăng nhập là bắt buộc.")]
		[StringLength(100, ErrorMessage = "Tên đăng nhập không được vượt quá 100 ký tự.")]
		public string Username { get; set; } = string.Empty;

		[Required(ErrorMessage = "Mật khẩu là bắt buộc.")]
		[StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu phải từ 6 đến 100 ký tự.")]
		public string Password { get; set; } = string.Empty;

		[Required(ErrorMessage = "Email là bắt buộc.")]
		[EmailAddress(ErrorMessage = "Định dạng Email không hợp lệ.")]
		[StringLength(150, ErrorMessage = "Email không được vượt quá 150 ký tự.")]
		public string Email { get; set; } = string.Empty;

		[StringLength(150, ErrorMessage = "Họ và tên không được vượt quá 150 ký tự.")]
		public string? Name { get; set; }

		[Required(ErrorMessage = "Số điện thoại là bắt buộc.")]
		[Phone(ErrorMessage = "Số điện thoại không hợp lệ.")]
		[StringLength(20, ErrorMessage = "Số điện thoại không được vượt quá 20 ký tự.")]
		public string PhoneNumber { get; set; } = string.Empty;

		public string? AvatarUrl { get; set; }
	}

	// Feature: Cập nhật thông tin cá nhân
	public class UpdateRequest
	{
		[StringLength(150, ErrorMessage = "Họ và tên không được vượt quá 150 ký tự.")]
		public string? Name { get; set; }

		[Phone(ErrorMessage = "Số điện thoại không hợp lệ.")]
		[StringLength(20, ErrorMessage = "Số điện thoại không được vượt quá 20 ký tự.")]
		public string? PhoneNumber { get; set; }

		public string? AvatarUrl { get; set; }
	}

	// Feature: Đổi mật khẩu
	public class LoginRequest
	{
		[Required(ErrorMessage = "Tên đăng nhập hoặc Email là bắt buộc.")]
		public string EmailOrUsername { get; set; } = string.Empty;

		[Required(ErrorMessage = "Mật khẩu là bắt buộc.")]
		public string Password { get; set; } = string.Empty;
    }
	public class LogoutRequest
	{
		[Required(ErrorMessage = "Định danh khách hàng là bắt buộc.")]
		public int CustomerId { get; set; }

		[Required(ErrorMessage = "Mã Refresh Token là bắt buộc.")]
		public string RefreshToken { get; set; } = string.Empty;
	}
	public class CustomerTokenQuery
	{
		public string? RefreshToken { get; set; }
		public int? CustomerId { get; set; }
		public bool IncludeRevoked { get; set; } = false;
		public bool OnlyActive { get; set; } = true;
	}
	/// <summary>
	/// Request gửi lên khi Access Token hết hạn để xin cấp bộ đôi mã mới
	/// </summary>
	public class RefreshTokenRequest
	{
		[Required(ErrorMessage = "Access Token cũ (đã hết hạn) là bắt buộc.")]
		public string ExpiredAccessToken { get; set; } = string.Empty;

		[Required(ErrorMessage = "Refresh Token đi kèm là bắt buộc.")]
		public string OldRefreshToken { get; set; } = string.Empty;
	}
	/// <summary>
	/// Request hướng đối tượng dùng để trao đổi thông tin giữa CustomerAuthService và CustomerRefreshTokenService
	/// </summary>
	public class TokenRotationRequest
	{
		public int CustomerId { get; set; }
		public string OldRefreshToken { get; set; } = string.Empty;
	}
	/// <summary>
	/// Request phục vụ luồng kiểm tra tính hợp lệ của Token dưới Database
	/// </summary>
	public class ValidationRequest
	{
		public string RefreshToken { get; set; } = string.Empty;
		public int CustomerId { get; set; }
	}
	public class ChangePasswordRequest
	{
		[Required(ErrorMessage = "Mật khẩu hiện tại là bắt buộc.")]
		public string OldPassword { get; set; } = string.Empty;

		[Required(ErrorMessage = "Mật khẩu mới là bắt buộc.")]
		[StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu mới phải từ 6 ký tự.")]
		public string NewPassword { get; set; } = string.Empty;

		[Compare("NewPassword", ErrorMessage = "Mật khẩu xác nhận không trùng khớp với mật khẩu mới.")]
		public string ConfirmPassword { get; set; } = string.Empty;
	}
}
