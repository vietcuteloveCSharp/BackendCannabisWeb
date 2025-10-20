
namespace Repository.Repository
{
	public class RoleRepository : BaseRepository<Role>, IRoleRepository
	{
		public RoleRepository(CannabisAccessorriesDBContext context) :base(context)
		{
			
		}

		public async Task<Role?> GetByNameAsync(string roleName)
		{
			return await _context.Roles.FirstOrDefaultAsync(r => r.RoleName.ToString() == roleName);
		}
	}
}
