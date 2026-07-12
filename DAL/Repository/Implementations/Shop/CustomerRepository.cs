

namespace DAL.Repository.Implementations.Shop
{
	internal class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
	{
		public CustomerRepository(CannabisAccessoriesDBContext context) : base(context)
		{
		}

		public async Task<bool> EmailExistsAsync(string email)
		{
			return await _context.Customers
				.AnyAsync(c => c.Email == email && c.IsDeleted == false);
		}

		public async Task<Customer?> GetByEmailAsync(string email)
		{
			return await _context.Customers
				.FirstOrDefaultAsync(c => c.Email == email && c.IsDeleted == false);
		}

		public async Task<Customer?> GetByUsernameAsync(string username)
		{
			return await _context.Customers
				.FirstOrDefaultAsync(c => c.Username == username && c.IsDeleted == false);
		}

		public async Task<bool> UserNameExistsAsync(string userName)
		{
			return await _context.Customers
				.AnyAsync(c => c.Username == userName && c.IsDeleted == false);
		}
	}
}
