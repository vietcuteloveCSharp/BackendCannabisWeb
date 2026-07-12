using DAL.Repository.BaseRepository;

namespace DAL.Repository.Interfaces.Shop
{
	public interface ICustomerSessionRepository : IBaseRepository<CustomerSession>
	{
		Task<CustomerSession?> GetByTokenAsync(string sessionToken);
		Task<List<CustomerSession>> GetActiveSessionsByCustomerIdAsync(int customerId);
	}
}
