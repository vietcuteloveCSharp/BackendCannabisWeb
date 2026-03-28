namespace Cannabis.Server.Controllers.Admin
{
	[ApiVersion("1.0")]
	[Route("api/v{version:apiVersion}/[controller]")]
	[ApiController]
	//[Authorize(Roles = "Admin")]
	public class AdminController : ControllerBase
	{
		private readonly IAdminService _adminService;
		public AdminController(IAdminService adminService)
		{
			this._adminService = adminService;
		}
		/// <summary>
		/// Registers a new user using the provided registration information.
		/// </summary>
		/// <param name="createAdminDTO">The user registration data.</param>
		/// <returns>
		/// 201 Created - Returns the newly created user's information.<br/>
		/// 400 Bad Request - If the input model is invalid.
		/// </returns>
		/// <response code="201">User registered successfully.</response>
		/// <response code="400">Invalid input data.</response>
		[HttpPost("create-admin")]
		//[Authorize(Roles = "Admin")]
		[ProducesResponseType(typeof(ApiResponse<UserDTO>), 201)]
		[ProducesResponseType(typeof(ApiResponse<object>), 400)]
		public async Task<IActionResult> CreateAdminAsync([FromBody] AdminCreateDTO createAdminDTO)
		{

			var result = await _adminService.RegisterAdminAsync(createAdminDTO);

			return CreatedAtAction(
				actionName: "GetUserById",
				controllerName: "User",
				routeValues: new { version = "1.0", id = result.Id },
				value: ApiResponse<object>.Ok(result, "Admin account created successfully")
				);
		}

		

		/// <summary>
		/// Cập nhật trạng thái người dùng (Active/Blocked).
		/// </summary>
		[HttpPatch("users/{id}/status")]
		[ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
		public async Task<IActionResult> UpdateUserStatus(int id, [FromBody] UserStatusUpdateDTO dto)
		{
			var success = await _adminService.UpdateUserStatusAsync(id, dto);
			if (!success) return BadRequest(ApiResponse<object>.Fail("Cập nhật trạng thái thất bại"));

			return Ok(ApiResponse<string>.Ok("Trạng thái người dùng đã được cập nhật thành công"));
		}

		/// <summary>
		/// Thay đổi quyền hạn (Role) của người dùng.
		/// </summary>
		[HttpPut("users/{id}/role")]
		[ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
		public async Task<IActionResult> ChangeUserRole(int id, [FromBody] UserRoleUpdateDTO dto)
		{
			var success = await _adminService.ChangeUserRoleAsync(id, dto);
			if (!success) return BadRequest(ApiResponse<object>.Fail("Thay đổi quyền hạn thất bại"));

			return Ok(ApiResponse<string>.Ok("Quyền hạn người dùng đã được cập nhật thành công"));
		}
	}
}
