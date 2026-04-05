using Service.IServices.UserManagement;

namespace Service.Services.UserManagement
{
	public class RoleService : BaseService<DAL.Entities.User.Role,RoleDTO,RoleCreateDTO,RoleUpdateDTO>,IRoleService
	{
		public RoleService(IUnitOfWork unitOfWork, IMapper mapper) :base(unitOfWork,mapper)
		{
			
		}
	}
}
