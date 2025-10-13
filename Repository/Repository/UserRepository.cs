
namespace Repository.Repository
{
	public class UserRepository : BaseRepository<User>, IUserRepository
	{
		public UserRepository(CannabisAccessorriesDBContext context) : base(context)
		{
			
		}
		public async Task<bool> EmailExistsAsync(string email)
		{
			return await _context.Users.AnyAsync(c => c.Email == email);
		}

		public async Task<User?> GetByEmailAsync(string email)
		{
			var user = await _context.Users.FirstOrDefaultAsync(c => c.Email == email);
			return user;
		}

		public async Task<User?> GetByUsernameAsync(string username)
		{
			var user = await _context.Users
				 .Include(u => u.Role)
				.FirstOrDefaultAsync(c => c.Username == username);
			return user;
		}

		

		public async Task<bool> UserNameExistsAsync(string userName)
		{
			return await _context.Users.AnyAsync(c => c.Username == userName);
		}
	}
}
