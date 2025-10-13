using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
	public interface IUserRepository :IBaseRepository<User>
	{
		Task<bool> EmailExistsAsync(string email);
		Task<bool> UserNameExistsAsync(string userName);
		Task<User?> GetByUsernameAsync(string username);
		Task<User?> GetByEmailAsync(string email);

	}
}
