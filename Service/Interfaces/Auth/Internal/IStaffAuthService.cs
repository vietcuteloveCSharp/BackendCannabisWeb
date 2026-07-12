using LoginRequest = Shared.DTOs.DTO.Internal.LoginRequest;
using LogoutRequest = Shared.DTOs.DTO.Internal.LogoutRequest;
using RefreshTokenRequest = Shared.DTOs.DTO.Internal.RefreshTokenRequest;

namespace Service.Interfaces.Auth.Internal
{
	public interface IStaffAuthService
	{
		/// <summary>
        /// Đăng nhập hệ thống quản trị nội bộ: Xác thực tài khoản -> Sinh mã Token bộ đôi -> Ghi vết Staff Session
        /// </summary>
        Task<TokenResponse> LoginAsync(LoginRequest request);

        /// <summary>
        /// Đăng xuất: Thu hồi Refresh Token và đóng phiên làm việc Staff Session
        /// </summary>
        Task LogoutAsync(LogoutRequest request);

        /// <summary>
        /// Xoay vòng đổi mã Access Token hết hạn bằng Refresh Token cho nhân viên
        /// </summary>
        Task<TokenResponse> RefreshTokenAsync(RefreshTokenRequest request);
	}
}
