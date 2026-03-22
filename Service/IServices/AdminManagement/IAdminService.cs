using DTO.DTOs.Admin.Admins;
using DTO.DTOs.User.Users;
using DTO.Response;
using static Enum.Domain.System_User;

namespace Service.IServices.AdminManagement
{
	public interface IAdminService
	{
		Task<UserDTO> RegisterAdminAsync(AdminCreateDTO createAdminDTO); //register account
		// 2. Quản lý người dùng (Bổ sung thêm)
		 Task<PagedResult<UserDTO>> GetAllUsersAsync(UserFilterDTO filter); // Xem danh sách có phân trang
		Task<bool> UpdateUserStatusAsync(int userId, UserStatusUpdateDTO status); // Khóa/Mở khóa tài khoản
		Task<bool> ChangeUserRoleAsync(int userId, UserRoleUpdateDTO roleDto);
	}
}
