

namespace Service.Implementations.UserManagement
{
	public class UserStatusService :BaseCRUDService<UserStatus, UserStatusDTO,UserStatusCreateDTO,UserStatusUpdateDTO>,IUserStatusService
	{
		public UserStatusService(IUnitOfWork unitOfWork,IMapper mapper) :base(unitOfWork,mapper)
		{
			
		}
	}
}
