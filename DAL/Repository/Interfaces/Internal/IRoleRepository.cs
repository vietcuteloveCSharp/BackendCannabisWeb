using DAL.Repository.BaseRepository;

namespace DAL.Repository.Interfaces.Internal
{
	public interface IRoleRepository : IBaseRepository<Role>
	{
		Task<Role?> GetByNameAsync(string roleName);
	}
}
