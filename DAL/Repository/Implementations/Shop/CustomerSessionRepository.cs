
namespace DAL.Repository.Implementations.Shop
{
	public class CustomerSessionRepository : BaseRepository<CustomerSession>, ICustomerSessionRepository
	{
		public CustomerSessionRepository(CannabisAccessoriesDBContext context) : base(context)
		{
		}

		public async Task<CustomerSession?> GetByTokenAsync(string sessionToken)
		{
			return await _context.CustomerSessions
				.FirstOrDefaultAsync(s => s.SessionToken == sessionToken && s.IsDeleted == false);
		}

		public async Task<List<CustomerSession>> GetActiveSessionsByCustomerIdAsync(int customerId)
		{
			return await _context.CustomerSessions
				.Where(s => s.CustomerId == customerId && s.ExpiresAt > DateTime.UtcNow && s.IsDeleted == false)
				.ToListAsync();
		}
	}
}
