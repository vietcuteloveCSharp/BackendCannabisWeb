using LoginRequest = Shared.DTOs.DTO.Shop.LoginRequest;
using LogoutRequest = Shared.DTOs.DTO.Shop.LogoutRequest;
using RefreshTokenRequest = Shared.DTOs.DTO.Shop.RefreshTokenRequest;

namespace Service.Interfaces.Auth.Shop
{
	public interface ICustomerAuthService
	{
		/// <summary>
		/// Đăng nhập Website: Xác thực tài khoản khách hàng -> Sinh mã Token bộ đôi -> Ghi vết Customer Session
		/// </summary>
		Task<CustomerTokenResponse> LoginAsync(LoginRequest request);

		/// <summary>
		/// Đăng xuất: Thu hồi Refresh Token và đóng phiên làm việc Customer Session công khai
		/// </summary>
		Task LogoutAsync(LogoutRequest request);

		/// <summary>
		/// Xoay vòng đổi mã Access Token hết hạn bằng Refresh Token cho khách hàng
		/// </summary>
		Task<CustomerTokenResponse> RefreshTokenAsync(RefreshTokenRequest request);
	}
}
