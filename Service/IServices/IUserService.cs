namespace Service.IServices
{
	public interface IUserService
	{
		Task<UserDTO?> GetUserByIdAsync(int id);
		Task<UserDTO?> UpdateAsync(int id,UpdateUserDTO userDto);
		Task<User?> FindUserByEmailAsync(string email);
	}
}
