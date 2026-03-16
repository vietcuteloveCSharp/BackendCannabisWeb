namespace Cannabis.Server.Controllers.Admin
{
	[ApiVersion("1.0")]
	[Route("api/v{version:apiVersion}/[controller]")]
	[ApiController]
	public class AdminController :ControllerBase
	{
		private readonly IAdminService _adminService;
		public AdminController(IAdminService adminService)
		{
			this._adminService=adminService;
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
		[Authorize(Roles = "Admin")]
		[ProducesResponseType(typeof(ApiResponse<UserDTO>), 201)]
		[ProducesResponseType(typeof(ApiResponse<object>),400)]
		public async Task<IActionResult> CreateAdminAsync([FromBody] AdminCreateDTO createAdminDTO)
		{
			// 1. Kiểm tra Model State tự động
			if (!ModelState.IsValid)
			{
				return this.ValidateModelState();
			}
			var result = await _adminService.RegisterAdminAsync(createAdminDTO);

			return CreatedAtAction(
				actionName: "GetUserById",
				controllerName: "User",
				routeValues: new { id = result.UserId },
				value: ApiResponse<object>.Ok(result, "Admin account created successfully")
				);
		}
	}
}
