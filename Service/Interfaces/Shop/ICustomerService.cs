using ChangePasswordRequest = Shared.DTOs.DTO.Shop.ChangePasswordRequest;
using RegisterRequest = Shared.DTOs.DTO.Shop.RegisterRequest;

namespace Service.Interfaces.Shop
{
	public interface ICustomerService
	{
		/// <summary>
		/// Đăng ký tài khoản khách hàng mua sắm ngoài giao diện cửa hàng
		/// </summary>
		Task<CustomerDTO> RegisterCustomerAsync(RegisterRequest request);

		/// <summary>
		/// Lấy thông tin hồ sơ cá nhân của khách hàng
		/// </summary>
		Task<CustomerDTO> GetProfileAsync(int customerId);

		/// <summary>
		/// Cập nhật thông tin cá nhân (Họ tên, Số điện thoại, Avatar...) của khách hàng
		/// </summary>
		Task<CustomerDTO> UpdateProfileAsync(int customerId, UpdateRequest request);

		/// <summary>
		/// Khách hàng tự thay đổi mật khẩu cá nhân
		/// </summary>
		Task<bool> ChangePasswordAsync(int customerId, ChangePasswordRequest request);
	}
}
