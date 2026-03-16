using DTO.DTOs.Admin.Admins;
using DTO.DTOs.User.Users;

namespace Service.IServices.AdminManagement
{
	public interface IAdminService
	{
		Task<UserDTO> RegisterAdminAsync(AdminCreateDTO createAdminDTO); //register account
	}
}
