using DAL.Entities.User;

namespace Repository.IRepository
{
	public interface IRoleRepository : IBaseRepository<Role>
	{
		Task<Role?> GetByNameAsync(string roleName);
	}
}
