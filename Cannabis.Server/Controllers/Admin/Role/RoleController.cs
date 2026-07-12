using Asp.Versioning;
using Cannabis.Server.Base;
using Service.Interfaces.Internal;

namespace Cannabis.Server.Controllers.Admin.Role
{
	[ApiVersion("1.0")]
	[Route("api/v{version:apiVersion}/admin/roles")]
	[Authorize(Roles = "Admin")]
	public class RoleController : BaseAdvancedController<DAL.Entities.Internal.Role, RoleDTO, RoleCreateDTO, RoleUpdateDTO>
	{
		private readonly IRoleService _roleService;

		public RoleController(IRoleService roleService) : base(roleService)
		{
			_roleService = roleService;
		}

		/// <summary>
		/// Lấy thông tin quyền hạn theo tên
		/// </summary>
		[HttpGet("name/{name}")]
		public async Task<IActionResult> GetByName(string name)
		{
			var result = await _roleService.GetByNameAsync(name);
			return result.Success ? Ok(result) : NotFound(result);
		}
	}
}
