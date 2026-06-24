using DAL.Repository.BaseRepository;

namespace DAL.Repository.Implementations
{
	public class RoleRepository : BaseRepository<Role>, IRoleRepository
	{
		public RoleRepository(CannabisAccessoriesDBContext context) :base(context)
		{
			
		}

		public async Task<Role?> GetByNameAsync(string roleName)
		{
			return await _context.Roles.FirstOrDefaultAsync(r => r.RoleName.ToString() == roleName);
		}
	}
}
