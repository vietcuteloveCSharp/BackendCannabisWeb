
namespace DAL.Repository.Implementations.Internal
{
	public class StaffStatusRepository :BaseRepository<StaffStatus>, IStaffStatusRepository
	{
		public StaffStatusRepository(CannabisAccessoriesDBContext context) : base(context)
		{
			
		}

		public async Task<StaffStatus?> GetByCodeAsync(string code)
		{
			return await _context.StaffStatuses.FirstOrDefaultAsync(r => r.Code.ToLower() == code.ToLower());
		}		
		
	}
}
