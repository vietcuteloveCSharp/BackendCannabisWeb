using RegisterRequest = Shared.DTOs.DTO.Internal.RegisterRequest;
using ChangePasswordRequest = Shared.DTOs.DTO.Internal.ChangePasswordRequest;

namespace Service.Interfaces.Internal
{
	public interface IStaffService
	{
		/// <summary>
		/// Đăng ký tài khoản nhân viên mới kèm gán mã StaffCode và quyền hạn
		/// </summary>
		Task<StaffDTO> RegisterStaffAsync(RegisterRequest request);

		/// <summary>
		/// Thay đổi mật khẩu nội bộ cho nhân viên
		/// </summary>
		Task<bool> ChangePasswordAsync(int staffId, ChangePasswordRequest request);

		/// <summary>
		/// Cập nhật trạng thái tài khoản nhân viên (Ví dụ: Active, Banned, Chờ duyệt)
		/// </summary>
		Task<bool> UpdateStaffStatusAsync(int staffId, int statusId);

		/// <summary>
		/// Thay đổi quyền hạn (Role) của nhân viên nội bộ
		/// </summary>
		Task<bool> ChangeStaffRoleAsync(int staffId, int newRoleId);
	}
}
