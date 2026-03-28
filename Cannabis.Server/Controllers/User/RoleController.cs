using System;

namespace Cannabis.Server.Controllers.User
{
	[ApiVersion("1.0")]
	[Route("api/v{version:apiVersion}/[controller]")]
	[ApiController]
	
	public class RoleController : ControllerBase
	{
		private readonly IRoleService _roleService;
		public RoleController(IRoleService roleService)
		{
			_roleService = roleService;
		}
		/// <summary>
		/// Creates a new role.
		/// </summary>
		/// <param name="createRoleDTO">The role data to create.</param>
		/// <returns>Returns the created role.</returns>
		/// <response code="201">Successfully created the role.</response>
		/// <response code="400">Invalid input data.</response>
		[HttpPost()]
		[ProducesResponseType(typeof(ApiResponse<object>), 200)]
		[ProducesResponseType(typeof(ApiResponse<object>), 400)]
		public async Task<IActionResult> CreateRoleAsync([FromBody] RoleCreateDTO createRoleDTO)
		{

			var createdRole = await _roleService.AddRoleAsync(createRoleDTO);
			var response = ApiResponse<object>.Ok(createdRole, "Role created successfully.");
			var version = (string?)Request.RouteValues["version"] ?? "1.0";
			var locationUrl = Url.Action(
				nameof(GetRoleByIdAsync),
					"Role",
				new { version, id = createdRole.Id },
				Request.Scheme);
			return Created(locationUrl!, ApiResponse<object>.Ok(createdRole, "Role created successfully."));
		}
		/// <summary>
		/// Retrieves the details of a specific role by ID.
		/// </summary>
		/// <param name="id">The unique identifier of the role.</param>
		/// <returns>The role details.</returns>
		/// <response code="200">Successfully retrieved the role.</response>
		/// <response code="404">Role not found.</response>
		[HttpGet("{id:int}")]
		[ProducesResponseType(typeof(ApiResponse<object>), 200)]
		[ProducesResponseType(typeof(ApiResponse<string>), 404)]
		public async Task<IActionResult> GetRoleByIdAsync(int id)
		{
			var role = await _roleService.GetRoleByIdAsync(id);
			return Ok(ApiResponse<object>.Ok(role!, "Role retrieved successfully."));
		}
		/// <summary>
		/// Retrieves all roles.
		/// </summary>
		/// <returns>An ApiResponse containing a list of roles.</returns>
		/// <response code="200">Successfully retrieved all roles.</response>
		/// 
		[HttpGet("")]
		[ProducesResponseType(typeof(ApiResponse<object>), 200)]
		public async Task<IActionResult> GetAllRolesAsync()
		{
			var roles = await _roleService.GetAllRolesAsync();
			return Ok(ApiResponse<object>.Ok(roles, "Roles retrieved successfully."));
		}
		
		// <summary>
		/// Updates an existing role by ID.
		/// </summary>
		/// <param name="id">The ID of the role to update.</param>
		/// <param name="updateRoleDTO">The updated role data.</param>
		/// <returns>An ApiResponse containing the updated role.</returns>
		/// <response code="200">Successfully updated the role.</response>
		/// <response code="400">Invalid input data.</response>
		/// <response code="404">Role not found.</response>
		[HttpPut("update/{id:int}")]
		[ProducesResponseType(typeof(ApiResponse<object>), 200)]
		[ProducesResponseType(typeof(ApiResponse<string>), 400)]
		[ProducesResponseType(typeof(ApiResponse<string>), 404)]
		public async Task<IActionResult> UpdateRoleAsync(int id, [FromBody] RoleUpdateDTO updateRoleDTO)
		{
			
			var updatedRole = await _roleService.UpdateRoleAsync(id, updateRoleDTO);
			return Ok(ApiResponse<object>.Ok(updatedRole!, "Role updated successfully."));
		}
		/// <summary>
		/// Deletes a role by ID.
		/// </summary>
		/// <param name="id">The ID of the role to delete.</param>
		/// <returns>An ApiResponse indicating success or failure.</returns>
		/// <response code="200">Successfully deleted the role.</response>
		/// <response code="404">Role not found.</response>
		//[HttpDelete("delete/{id:int}")]
		//[Authorize(Roles = "Admin")]
		//[ProducesResponseType(typeof(ApiResponse<string>), 200)]
		//[ProducesResponseType(typeof(ApiResponse<string>), 404)]
		//public async Task<IActionResult> DeleteRoleAsync(int id)
		//{
		//	var deleted = await _roleService.(id);
		//	if (!deleted)
		//		return NotFound(ApiResponse<string>.Fail($"Role with ID {id} not found."));

		//	return Ok(ApiResponse<string>.Ok(default!, "Role deleted successfully."));
		//}
	}
}
