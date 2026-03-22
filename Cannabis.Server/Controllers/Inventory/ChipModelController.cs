using DTO.DTOs.ChipModels;
using Service.IServices.Inventory;

namespace Cannabis.Server.Controllers.Inventory
{
	[ApiVersion("1.0")]
	[Route("api/v{version:apiVersion}/[controller]")]
	[ApiController]
	[Authorize(Roles ="Admin")]
	public class ChipModelController : ControllerBase
	{
		private readonly IChipModelService _chipModelService;
		public ChipModelController(IChipModelService chipModelService)
		{
			this._chipModelService = chipModelService;
		}
		/// <summary>
		/// Get all chip models.
		/// </summary>
		/// <returns>List of chip models.</returns>
		[HttpGet]
		[ProducesResponseType(typeof(ApiResponse<IEnumerable<ChipModelDTO>>), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetAllAsync()
		{
			var data = await _chipModelService.GetAllAsync();
			return Ok(ApiResponse<IEnumerable<ChipModelDTO>>.Ok(data));
		}
		/// <summary>
		/// Get all chip models active.
		/// </summary>
		/// <returns>List of chip models.</returns>
		[HttpGet("active")]
		[ProducesResponseType(typeof(ApiResponse<IEnumerable<ChipModelDTO>>), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetAllActiveAsync()
		{
			var data = await _chipModelService.GetAllActiveAsync();
			return Ok(ApiResponse<IEnumerable<ChipModelDTO>>.Ok(data));
		}
		/// <summary>
		/// Get chip model by its ID.
		/// </summary>
		/// <param name="id">Chip model ID.</param>
		/// <returns>Chip model detail.</returns>
		[HttpGet("{id:int}")]
		[ProducesResponseType(typeof(ApiResponse<ChipModelDTO>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetByIdAsync(int id)
		{
			var data = await _chipModelService.GetByIdAsync(id);
			if (data == null) // Check null thay vì dùng data!
				return NotFound(ApiResponse<string>.Fail("Chip model not found."));
			return Ok(ApiResponse<ChipModelDTO>.Ok(data!));
		}
		/// <summary>
		/// Create a new chip model.
		/// </summary>
		/// <param name="dto">Chip model creation DTO.</param>
		/// <returns>Newly created chip model.</returns>
		[HttpPost]
		[Authorize(Roles = "Admin")]
		[ProducesResponseType(typeof(ApiResponse<ChipModelDTO>), StatusCodes.Status201Created)]
		[ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> CreateAsync([FromBody] ChipModelCreateDTO dto)
		{
			var created = await _chipModelService.CreateAsync(dto);
			if (created == null)
				return BadRequest(ApiResponse<string>.Fail("Failed to create chip model"));

			return CreatedAtAction(nameof(GetByIdAsync), new { id = created.ChipModelId }, ApiResponse<ChipModelDTO>.Ok(created));
		}

		/// <summary>
		/// Update an existing chip model.
		/// </summary>
		/// <param name="id">Chip model ID.</param>
		/// <param name="dto">Chip model update DTO.</param>
		/// <returns>Update result.</returns>
		[HttpPut("{id:int}")]
		[Authorize(Roles = "Admin")]
		[ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
		public async Task<IActionResult> UpdateAsync(int id, [FromBody] ChipModelUpdateDTO dto)
		{
			var updated = await _chipModelService.UpdateAsync(id, dto);
			if (!updated)
				return NotFound(ApiResponse<string>.Fail("Chip model not found or update failed"));

			return Ok(ApiResponse<bool>.Ok(true, "Updated successfully"));
		}
		/// <summary>
		/// Soft delete Chip model
		/// </summary>
		[HttpDelete("{id:int}")]
		[Authorize(Roles = "Admin")]
		[ProducesResponseType(typeof(ApiResponse<bool>), 200)]
		[ProducesResponseType(404)]
		public async Task<IActionResult> Delete(int id)
		{
			var success = await _chipModelService.DeleteAsync(id);

			if (!success)
				return NotFound(ApiResponse<bool>.Fail("Chip model not found."));

			return Ok(ApiResponse<bool>.Ok(true, "Deleted successfully."));
		}

	}
}
