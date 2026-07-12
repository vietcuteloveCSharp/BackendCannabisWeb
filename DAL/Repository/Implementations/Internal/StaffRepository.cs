
namespace DAL.Repository.Implementations.Internal
{
	public class StaffRepository : BaseRepository<Staff>, IStaffRepository
	{
		public StaffRepository(CannabisAccessoriesDBContext context) : base(context)
		{ 

		}
		public async Task<bool> EmailExistsAsync(string email)
		{
			return await _context.Staffs // Đổi từ Users sang Staffs
				.AsNoTracking()
				.AnyAsync(s => s.Email == email && s.IsDeleted == false);
		}

		public async Task<Staff?> GetByEmailAsync(string email)
		{
			return await _context.Staffs
				.AsNoTracking()
				.FirstOrDefaultAsync(s => s.Email == email && s.IsDeleted == false);
		}

		public async Task<Staff?> GetByUsernameAsync(string username)
		{
			return await _context.Staffs
				 .Include(s => s.Role)
				 .Include(s => s.Status)
				 .AsNoTracking()
				.FirstOrDefaultAsync(s => s.Username == username && s.IsDeleted == false);
		}

		public async Task<bool> UserNameExistsAsync(string userName)
		{
			return await _context.Staffs
				.AsNoTracking()
				.AnyAsync(s => s.Username == userName && s.IsDeleted == false);
		}

		public async Task<Staff?> GetByStaffCodeAsync(string staffCode)
		{
			return await _context.Staffs
				.AsNoTracking()
				.FirstOrDefaultAsync(s => s.StaffCode == staffCode && s.IsDeleted == false);
		}
		
	}
}
	

