using DAL.Repository.BaseRepository;

namespace DAL.Repository.Implementations
{
	public class UserStatusRepository :BaseRepository<UserStatus>,IUserStatusRepository
	{
		public UserStatusRepository(CannabisAccessoriesDBContext context) : base(context)
		{
			
		}

		public async Task<UserStatus?> GetCodeAsync(string code)
		{
			return await _context.UserStatuses.FirstOrDefaultAsync(r => r.Code.ToString().ToLower() == code);
		}
	}
}
