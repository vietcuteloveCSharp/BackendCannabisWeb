

namespace Cannabis.Server.Controllers.User
{
	public class UserStatusController : BaseCrudController<UserStatus, UserStatusDTO,UserStatusCreateDTO,UserStatusUpdateDTO>
	{
		public UserStatusController(IUserStatusService userStatusService) :base(userStatusService)
		{
			
		}
	}
}
