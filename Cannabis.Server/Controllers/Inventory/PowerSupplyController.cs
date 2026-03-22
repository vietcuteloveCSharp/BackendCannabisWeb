using DAL.Entities;
using DTO.DTOs.PowerSupplies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.IServices.Inventory;

namespace Cannabis.Server.Controllers.Inventory
{
	[ApiVersion("1.0")]
	[Route("api/v{version:apiVersion}/[controller]")]
	[ApiController]
	[Authorize]
	public class PowerSupplyController : ControllerBase
	{
		private readonly IPowerSupplyService _powerSupplyService;
		public PowerSupplyController(IPowerSupplyService powerSupplyService)
		{
			this._powerSupplyService = powerSupplyService;
		}
		/// <summary>
		/// Get all power supplies.
		/// </summary>
		[HttpGet]
		[Authorize(Roles="Admin")]
		[ProducesResponseType(typeof(ApiResponse<IEnumerable<PowerSupplyDTO>>), 200)]
		public async Task<IActionResult> GetAllAsync()
		{
			var data = await _powerSupplyService.GetAllAsync();
			return Ok(ApiResponse<IEnumerable<PowerSupplyDTO>>.Ok(data));
		}
		/// <summary>
		/// Get all power supplies active.
		/// </summary>
		[HttpGet("active")]
		[ProducesResponseType(typeof(ApiResponse<IEnumerable<PowerSupplyDTO>>), 200)]
		public async Task<IActionResult> GetAllActiveAsync()
		{
			var data = await _powerSupplyService.GetAllActiveAsync();
			return Ok(ApiResponse<IEnumerable<PowerSupplyDTO>>.Ok(data));
		}
		/// <summary>
		/// Get power supply by id.
		// <param name="id"> Power supply ID.</param>
		/// </summary>
		[HttpGet("{id:int}")]
		[ProducesResponseType(typeof(ApiResponse<PowerSupplyDTO>), 200)]
		[ProducesResponseType(404)]
		public async Task<IActionResult> GetById(int id)
		{
			var result = await _powerSupplyService.GetByIdAsync(id);
			if (result == null)
				return NotFound(ApiResponse<string>.Fail("Power supply not found."));

			return Ok(ApiResponse<PowerSupplyDTO>.Ok(result));
		}

		/// <summary>
		/// Create new power supply.
		/// </summary>
		[HttpPost]
		[Authorize(Roles = "Admin")]
		[ProducesResponseType(typeof(ApiResponse<PowerSupplyDTO>), 201)]
		[ProducesResponseType(404)]
		public async Task<IActionResult> Create([FromBody] PowerSupplyCreateDTO dto)
		{
			var created = await _powerSupplyService.AddAsync(dto);

			return CreatedAtAction(
				nameof(GetById),
				new { id = created.PowerSupplyId },
				ApiResponse<PowerSupplyDTO>.Ok(created, "Created successfully.")
			);
		}

		/// <summary>
		/// Update power supply.
		/// </summary>
		[HttpPut("{id:int}")]
		[Authorize(Roles = "Admin")]
		[ProducesResponseType(typeof(ApiResponse<bool>), 200)]
		[ProducesResponseType(404)]
		public async Task<IActionResult> Update(int id, [FromBody] PowerSupplyUpdateDTO dto)
		{
			var updated = await _powerSupplyService.UpdateAsync(id, dto);

			if (!updated)
				return NotFound(ApiResponse<string>.Fail("Power supply not found."));

			return Ok(ApiResponse<bool>.Ok(true, "Updated successfully."));
		}
		/// <summary>
		/// Soft delete powersupply
		/// </summary>
		[HttpDelete("{id:int}")]
		[ProducesResponseType(typeof(ApiResponse<bool>), 200)]
		[ProducesResponseType(404)]
		public async Task<IActionResult> Delete(int id)
		{
			var success = await _powerSupplyService.DeleteAsync(id);

			if (!success)
				return NotFound(ApiResponse<bool>.Fail("Power supply not found."));

			return Ok(ApiResponse<bool>.Ok(true, "Deleted successfully."));
		}
	}
}
