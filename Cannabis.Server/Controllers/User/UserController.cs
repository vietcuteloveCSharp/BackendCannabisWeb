namespace Cannabis.Server.Controllers.User
{
	[ApiVersion("1.0")]
	[Route("api/v{version:apiVersion}/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserService _UserService;
		public UserController(IUserService UserService)
		{
			_UserService = UserService;
		}
		/// <summary>
		/// Retrieves a user by their unique identifier.
		/// </summary>
		/// <param name="id">The ID of the user to retrieve.</param>
		/// <returns>
		/// - <see cref="StatusCodes.Status200OK"/>: Successfully retrieved the user data.<br/>
		/// - <see cref="StatusCodes.Status404NotFound"/>: No user found with the specified ID.<br/>
		/// - <see cref="StatusCodes.Status400BadRequest"/>: The provided ID is invalid (e.g., negative or zero).<br/>
		/// - <see cref="StatusCodes.Status500InternalServerError"/>: An unexpected error occurred on the server.
		/// </returns>
		/// <response code="200">User data was successfully retrieved.</response>
		/// <response code="400">The request was invalid.</response>
		/// <response code="404">User not found.</response>
		/// <response code="500">Internal server error.</response>
		/// <exception cref="NotFoundException">
		/// Thrown when the user with the specified ID does not exist.
		/// </exception>
		[HttpGet("{id:int}")]
		[Authorize(Roles="Admin")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		
		public async Task<IActionResult> GetUserById(int id)
		{
			var user = await _UserService.GetUserByIdAsync(id);
			return Ok(user);
		}
		/// <summary>
		/// Registers a new user using the provided registration information.
		/// </summary>
		/// <param name="createUserDTO">The user registration data.</param>
		/// <returns>
		/// 201 Created - Returns the newly created user's information.<br/>
		/// 400 Bad Request - If the input model is invalid.
		/// </returns>
		/// <response code="201">User registered successfully.</response>
		/// <response code="400">Invalid input data.</response>
		[HttpPost("register-user")]
		[AllowAnonymous]
		[ProducesResponseType(201)]
		[ProducesResponseType(400)]
		public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserDTO createUserDTO)
		{
			
			var result = await _UserService.RegisterUserAsync(createUserDTO);

			return CreatedAtAction(
				actionName: "GetUserById",
				controllerName: "User",
				routeValues: new { version = "1.0",id = result.Id },
				value: ApiResponse<object>.Ok(result, "User registered successfully")
				);
		}
	}
}
