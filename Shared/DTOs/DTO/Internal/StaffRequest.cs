
namespace Shared.DTOs.DTO.Internal
{
	// Feature: Admin tạo mới nhân viên
	public class RegisterRequest
	{
		[Required(ErrorMessage = "Mã nhân viên là bắt buộc.")]
		[StringLength(50, ErrorMessage = "Mã nhân viên không vượt quá 50 ký tự.")]
		public string StaffCode { get; set; } = string.Empty;

		[Required(ErrorMessage = "Tên tài khoản là bắt buộc.")]
		[StringLength(100, ErrorMessage = "Tên tài khoản không vượt quá 100 ký tự.")]
		public string Username { get; set; } = string.Empty;

		[Required(ErrorMessage = "Mật khẩu là bắt buộc.")]
		[StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu phải từ 6 đến 100 ký tự.")]
		public string Password { get; set; } = string.Empty;

		[Required(ErrorMessage = "Email là bắt buộc.")]
		[EmailAddress(ErrorMessage = "Định dạng Email không hợp lệ.")]
		[StringLength(150, ErrorMessage = "Email không vượt quá 150 ký tự.")]
		public string Email { get; set; } = string.Empty;

		[StringLength(150, ErrorMessage = "Họ tên không vượt quá 150 ký tự.")]
		public string? Name { get; set; }

		[Phone(ErrorMessage = "Số điện thoại không hợp lệ.")]
		[StringLength(20, ErrorMessage = "Số điện thoại không vượt quá 20 ký tự.")]
		public string PhoneNumber { get; set; } = string.Empty;

		public string? AvatarUrl { get; set; }

		[Required(ErrorMessage = "Vui lòng chọn Quyền hạn.")]
		public int RoleId { get; set; }

		[Required(ErrorMessage = "Vui lòng chọn Trạng thái.")]
		public int StatusId { get; set; }
	}

	// Feature: Thay đổi trạng thái tài khoản nhân viên (Block/Active)
	public class StatusUpdateRequest
	{
		[Required(ErrorMessage = "Vui lòng chọn trạng thái mới.")]
		public int StatusId { get; set; }
	}

	// Feature: Admin điều chỉnh quyền hạn (Role) của nhân viên cấp dưới
	public class RoleUpdateRequest
	{
		[Required(ErrorMessage = "Vui lòng chọn quyền hạn mới.")]
		public int NewRoleId { get; set; }
	}

	// Feature: Nhân viên tự thay đổi mật khẩu
	public class ChangePasswordRequest
	{
		[Required(ErrorMessage = "Mật khẩu cũ là bắt buộc.")]
		public string OldPassword { get; set; } = string.Empty;

		[Required(ErrorMessage = "Mật khẩu mới là bắt buộc.")]
		[StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu mới phải từ 6 đến 100 ký tự.")]
		public string NewPassword { get; set; } = string.Empty;

		[Required(ErrorMessage = "Xác nhận mật khẩu mới là bắt buộc.")]
		[Compare("NewPassword", ErrorMessage = "Mật khẩu xác nhận không trùng khớp.")]
		public string ConfirmPassword { get; set; } = string.Empty;
	}

	// Feature: Đăng nhập hệ thống quản trị (Backoffice Login)
	public class LoginRequest
	{
		[Required(ErrorMessage = "Tên đăng nhập là bắt buộc.")]
		public string Username { get; set; } = string.Empty;

		[Required(ErrorMessage = "Mật khẩu là bắt buộc.")]
		public string Password { get; set; } = string.Empty;
	}
	public class TokenQuery
	{
		public string? RefreshToken { get; set; }
		public int? StaffId { get; set; }
		public bool IncludeRevoked { get; set; } = false;
		public bool OnlyActive { get; set; } = true;
	}
	public class TokenRotationRequest
	{
		public int StaffId { get; set; }
		public string OldRefreshToken { get; set; } = string.Empty;
	}
	public class LogoutRequest
	{
		public int StaffId { get; set; }
		public string RefreshTokenValue { get; set; } = string.Empty;

	}
	public class RefreshTokenRequest
	{
		public string ExpiredAccessToken { get; set; } = string.Empty;
		public string OldRefreshToken { get; set; } =string.Empty;
	}
	public class SessionTokenRequest
	{
		public string SessionToken { get; set; } = string.Empty;
		public bool TrackChanges { get; set; } = false;
	}
	public class GetSessionTokenRequest
	{
		public int StaffId { get; set; }
		public bool TrackChanges { get; set; } = false;
	}
	public class ValidationRequest
	{
		public string RefreshToken { get; set; } = string.Empty; 
		public int StaffId { get;set; }

	}
}
