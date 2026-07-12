

namespace DAL.Repository.Interfaces.Shop
{
	public interface ICustomerRepository : IBaseRepository<Customer>
	{
		Task<bool> EmailExistsAsync(string email);
		Task<bool> UserNameExistsAsync(string userName);
		Task<Customer?> GetByUsernameAsync(string username);
		Task<Customer?> GetByEmailAsync(string email);
	}
}
