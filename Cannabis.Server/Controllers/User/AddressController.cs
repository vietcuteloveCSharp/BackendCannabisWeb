namespace Cannabis.Server.Controllers.User
{
	[ApiVersion("1.0")]
	[Route("api/v{version:apiVersion}/[controller]")]
	[ApiController]
	[Authorize]
	public class AddressController : ControllerBase
	{
		private readonly IAddressService _addressService;
		public AddressController(IAddressService addressService)
		{
			_addressService = addressService;
		}
		/// <summary>
		/// Creates an addresss
		/// </summary>
		[HttpPost()]
		[ProducesResponseType(typeof(ApiResponse<object>), 200)]
		[ProducesResponseType(typeof(ApiResponse<object>), 400)]
		public async Task<IActionResult> CreateAddressAsync([FromBody] RoleCreateDTO createRoleDTO)
		{

			var createdRole = await _roleService.AddRoleAsync(createRoleDTO);
			var response = ApiResponse<object>.Ok(createdRole, "Role created successfully.");
			var version = (string?)Request.RouteValues["version"] ?? "1.0";
			var locationUrl = Url.Action(
				nameof(GetRoleByIdAsync),
					"Role",
				new { version, id = createdRole.RoleId },
				Request.Scheme);
			return Created(locationUrl!, ApiResponse<object>.Ok(createdRole, "Role created successfully."));
		}

	}
}
