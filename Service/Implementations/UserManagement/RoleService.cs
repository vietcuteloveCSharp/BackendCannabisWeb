namespace Service.Implementations.UserManagement
{
	public class RoleService : BaseCRUDService<DAL.Entities.User.Role,RoleDTO,RoleCreateDTO,RoleUpdateDTO>,IRoleService
	{
		public RoleService(IUnitOfWork unitOfWork, IMapper mapper) :base(unitOfWork,mapper)
		{
			
		}
	}
}
