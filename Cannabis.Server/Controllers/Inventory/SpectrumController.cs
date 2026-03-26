using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cannabis.Server.Controllers.Inventory
{
	[ApiVersion("1.0")]
	[Route("api/v{version:apiVersion}/[controller]")]
	[ApiController]
	
	public class SpectrumController : ControllerBase
	{
		private readonly ISpectrumService _spectrumService;

		public SpectrumController(ISpectrumService spectrumService)
		{
			_spectrumService = spectrumService;
		}
		/// <summary>
		/// Get all spectrums including deleted ones.
		/// </summary>
		/// <returns>List of SpectrumDTO</returns>
		[HttpGet]
		[ProducesResponseType(typeof(ApiResponse<IEnumerable<SpectrumDTO?>>), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetAllAsync()
		{
			var result = await _spectrumService.GetAllAsync();
			return Ok(ApiResponse<IEnumerable<SpectrumDTO?>>.Ok(result));
		}

		/// <summary>
		/// Get all active (not deleted) spectrums.
		/// </summary>
		/// <returns>List of active SpectrumDTO</returns>
		[HttpGet("active")]
		[ProducesResponseType(typeof(ApiResponse<IEnumerable<SpectrumDTO?>>), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetAllActiveAsync()
		{
			var result = await _spectrumService.GetAllActiveAsync();
			return Ok(ApiResponse<IEnumerable<SpectrumDTO?>>.Ok(result));
		}

		/// <summary>
		/// Get spectrum by ID.
		/// </summary>
		/// <param name="id">Spectrum ID</param>
		/// <returns>SpectrumDTO</returns>
		[HttpGet("{id:int}")]
		[ProducesResponseType(typeof(ApiResponse<SpectrumDTO?>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetByIdAsync(int id)
		{
			var exists = await _spectrumService.ExistAsync(id);
			if (!exists)
				return NotFound(ApiResponse<string>.Fail("Spectrum not found."));

			var spectrum = await _spectrumService.GetByIdAsync(id);
			return Ok(ApiResponse<SpectrumDTO?>.Ok(spectrum));
		}

		/// <summary>
		/// Create a new spectrum.
		/// </summary>
		/// <param name="dto">Spectrum create DTO</param>
		/// <returns>Created SpectrumDTO</returns>
		[HttpPost]
		[ProducesResponseType(typeof(ApiResponse<SpectrumDTO>), StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> CreateAsync([FromForm] SpectrumCreateDTO dto , ApiVersion version)
		{
			var created = await _spectrumService.AddAsync(dto);

			return Ok(ApiResponse<SpectrumDTO>.Ok(created, "Spectrum created successfully."));
		}

		/// <summary>
		/// Update an existing spectrum.
		/// </summary>
		/// <param name="id">Spectrum ID</param>
		/// <param name="dto">Update DTO</param>
		/// <returns>Boolean result</returns>
		[HttpPut("{id:int}")]
		[ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> UpdateAsync(int id, [FromForm] SpectrumUpdateDTO dto)
		{
			
			var updated = await _spectrumService.UpdateAsync(id, dto);
			return Ok(ApiResponse<bool>.Ok(updated, "Spectrum updated successfully."));
		}

		/// <summary>
		/// Soft delete a spectrum (mark as deleted).
		/// </summary>
		/// <param name="id">Spectrum ID</param>
		/// <returns>Boolean result</returns>
		[HttpDelete("{id:int}")]
		[ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> DeleteAsync(int id)
		{
			var deleted = await _spectrumService.DeleteAsync(id);

			if (!deleted)
				return NotFound(ApiResponse<string>.Fail("Spectrum not found or already deleted."));

			return Ok(ApiResponse<bool>.Ok(true, "Spectrum deleted successfully."));
		}
	}
}
