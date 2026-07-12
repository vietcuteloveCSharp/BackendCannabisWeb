
namespace DAL.Repository.Interfaces.Internal
{
	public interface IStaffRepository :IBaseRepository<Staff>
	{
		Task<bool> EmailExistsAsync(string email);
		Task<bool> UserNameExistsAsync(string userName);
		Task<Staff?> GetByUsernameAsync(string username);
		Task<Staff?> GetByEmailAsync(string email);
		Task<Staff?> GetByStaffCodeAsync(string staffCode);
	}
}
