using DAL.Entities.User;
using DAL.Repository.BaseRepository;

namespace DAL.Repository.Interfaces
{
	public interface IRoleRepository : IBaseRepository<Role>
	{
		Task<Role?> GetByNameAsync(string roleName);
	}
}
