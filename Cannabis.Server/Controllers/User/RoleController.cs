using Org.BouncyCastle.Crypto;

namespace Cannabis.Server.Controllers.User
{
	[ApiVersion("1.0")]
	[Route("api/v{version:apiVersion}/[controller]")]
	[ApiController]

	public class RoleController : BaseApiController<Role, RoleDTO, RoleCreateDTO, RoleUpdateDTO>
	{
		public RoleController(IRoleService roleService) : base(roleService)
		{
			{

			}

		}
		[NonAction]
		public override async Task<IActionResult> Delete(int id)
		{
			return await base.Delete(id);
		}
		[NonAction]
		public override async Task<IActionResult> DeleteMany([FromBody] List<int> ids)
		{
			return await base.DeleteMany(ids);
		}
		[NonAction]
		public override async Task<IActionResult> HardDelete(int id)
		{
			return await base.HardDelete(id);
		}

		[NonAction]
		public override async Task<IActionResult> Restore(int id)
		{
			return await base.Restore(id);
		}

		[NonAction]
		public override async Task<IActionResult> RestoreMany([FromBody] List<int> ids)
		{
			return await base.RestoreMany(ids);
		}
	}
}
