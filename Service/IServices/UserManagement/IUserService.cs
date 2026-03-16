using DTO.DTOs.User.Users;

namespace Service.IServices.UserManagement
{
	public interface IUserService
	{
		Task<UserDTO?> GetUserByIdAsync(int id);
		Task<UserDTO?> UpdateAsync(int id,UpdateUserDTO userDto);
		Task<User?> FindUserByEmailAsync(string email);
		Task<UserDTO> RegisterUserAsync(CreateUserDTO createUserDTO); //register account
		Task<bool> ChangePasswordAsync(int userId, ChangePasswordDTO changePasswordDto);
	}
}
