



namespace Cannabis.Server.Controllers.User
{
	[ApiVersion("1.0")]
	public class RoleController : BaseCrudController<Role, RoleDTO, RoleCreateDTO, RoleUpdateDTO>
	{
		public RoleController(IRoleService roleService) : base(roleService)
		{
			{

			}

		}
		
	}
}
