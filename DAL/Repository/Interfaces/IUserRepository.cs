namespace DAL.Repository.Interfaces
{
	public interface IUserRepository :IBaseRepository<User>
	{
		Task<bool> EmailExistsAsync(string email);
		Task<bool> UserNameExistsAsync(string userName);
		Task<User?> GetByUsernameAsync(string username);
		Task<User?> GetByEmailAsync(string email);

	}
}
