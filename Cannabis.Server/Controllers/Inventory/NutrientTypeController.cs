using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.IServices.Inventory;
using Service.Services.Inventory;


namespace Cannabis.Server.Controllers.Inventory
{
	[ApiVersion("1.0")]
	[Route("api/v{version:apiVersion}/[controller]")]
	[ApiController]
	[Authorize]
	public class NutrientTypeController(INutrientTypeService nutrientTypeService) : ControllerBase
	{
		private readonly INutrientTypeService _nutrientTypeService = nutrientTypeService;
		/// <summary>
		/// Get all nutrient type
		/// </summary>
		[HttpGet]
		[ProducesResponseType(typeof(ApiResponse<IEnumerable<NutrientTypeDTO>>), 200)]
		public async Task<IActionResult> GetAllAsync()
		{
			var data = await _nutrientTypeService.GetAllAsync();
			return Ok(ApiResponse<IEnumerable<NutrientTypeDTO>>.Ok(data));
		}
		/// <summary>
		/// Get all nutrient type active
		/// </summary>
		[HttpGet("active")]
		[ProducesResponseType(typeof(ApiResponse<IEnumerable<NutrientTypeDTO>>), 200)]
		public async Task<IActionResult> GetAllActiveAsync()
		{
			var data = await _nutrientTypeService.GetAllActiveAsync();
			return Ok(ApiResponse<IEnumerable<NutrientTypeDTO>>.Ok(data));
		}
		/// <summary>
		/// Get nutrient type by id.
		/// <param name="id">Nutrient Type ID.</param>
		/// </summary>
		[HttpGet("{id:int}")]
		[ProducesResponseType(typeof(ApiResponse<NutrientTypeDTO>), 200)]
		[ProducesResponseType(404)]
		public async Task<IActionResult> GetById(int id)
		{
			var result = await _nutrientTypeService.GetByIdAsync(id);
			if (result == null)
				return NotFound(ApiResponse<NutrientTypeDTO>.Fail("Nutrient type not found."));

			return Ok(ApiResponse<NutrientTypeDTO>.Ok(result));
		}

		/// <summary>
		/// Create new nutrient type
		/// </summary>
		[HttpPost]
		[ProducesResponseType(typeof(ApiResponse<NutrientTypeDTO>), 201)]
		[ProducesResponseType(404)]
		public async Task<IActionResult> Create([FromBody] NutrientTypeCreateDTO dto)
		{
			var created = await _nutrientTypeService.CreateAsync(dto);

			return Ok(
				ApiResponse<NutrientTypeDTO>.Ok(created, "Created successfully.")
			);
		}

		/// <summary>
		/// Update power supply.
		/// </summary>
		[HttpPut("{id:int}")]
		[Authorize(Roles = "Admin")]
		[ProducesResponseType(typeof(ApiResponse<bool>), 200)]
		[ProducesResponseType(404)]
		public async Task<IActionResult> Update(int id, [FromBody] NutrientTypeUpdateDTO dto)
		{
			var updated = await _nutrientTypeService.UpdateAsync(id, dto);

			if (!updated)
				return NotFound(ApiResponse<string>.Fail("Nutrient type not found."));

			return Ok(ApiResponse<bool>.Ok(true, "Updated successfully."));
		}
		/// <summary>
		/// Soft delete Nutrient type
		/// </summary>
		[HttpDelete("{id:int}")]
		[ProducesResponseType(typeof(ApiResponse<bool>), 200)]
		[ProducesResponseType(404)]
		public async Task<IActionResult> Delete(int id)
		{
			var success = await _nutrientTypeService.DeleteAsync(id);

			if (!success)
				return NotFound(ApiResponse<bool>.Fail("Nutrient type not found."));

			return Ok(ApiResponse<bool>.Ok(true, "Deleted successfully."));
		}
	}
}
