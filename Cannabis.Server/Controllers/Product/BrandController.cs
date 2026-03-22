using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.IServices.Product;

namespace Cannabis.Server.Controllers.Product
{
	[ApiVersion("1.0")]
	[Route("api/v{version:apiVersion}/[controller]")]
	[ApiController]
	public class BrandController(IBrandService brandService) : ControllerBase
	{
		private readonly IBrandService _brandService =brandService;
		/// <summary>
		/// Get all brands.
		/// </summary>
		/// <returns>List of all brands.</returns>
		[HttpGet()]
		[ProducesResponseType(typeof(ApiResponse<IEnumerable<BrandDTO>>), 200)]
		[ProducesResponseType(typeof(ApiResponse<string>), 500)]
		public async Task<IActionResult> GetAllAsync()
		{
			var brands = await _brandService.GetAllAsync();
			return Ok(ApiResponse<IEnumerable<BrandDTO>>.Ok(brands));
		}
		/// <summary>
		/// Get all brands active.
		/// </summary>
		/// <returns>List of all brands.</returns>
		[HttpGet("active")]
		[ProducesResponseType(typeof(ApiResponse<IEnumerable<BrandDTO>>), 200)]
		[ProducesResponseType(typeof(ApiResponse<string>), 500)]
		public async Task<IActionResult> GetAllActiveAsync()
		{
			var brands = await _brandService.GetAllActiveAsync();
			return Ok(ApiResponse<IEnumerable<BrandDTO>>.Ok(brands));
		}
		/// <summary>
		/// Get a brand by its ID.
		/// </summary>
		/// <param name="id">Brand ID</param>
		/// <returns>Brand data.</returns>
		[HttpGet("{id:int}")]
		[ProducesResponseType(typeof(ApiResponse<BrandDTO>), 200)]
		[ProducesResponseType(typeof(ApiResponse<string>), 404)]
		[ProducesResponseType(typeof(ApiResponse<string>), 500)]
		public async Task<IActionResult> GetById(int id)
		{
			var brand = await _brandService.GetByIdAsync(id);
			return Ok(ApiResponse<BrandDTO>.Ok(brand!));
		}
		  /// <summary>
        /// Add a new brand.
        /// </summary>
        /// <param name="brandDTO">Brand creation data</param>
        /// <returns>Created brand.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<BrandDTO>), 201)]
        [ProducesResponseType(typeof(ApiResponse<string>), 400)]
        [ProducesResponseType(typeof(ApiResponse<string>), 500)]
        public async Task<IActionResult> Add([FromBody] BrandCreateDTO brandDTO)
        {
            var brand = await _brandService.AddAsync(brandDTO);
            if (brand == null)
                return BadRequest(ApiResponse<string>.Fail("Brand creation failed"));
            return CreatedAtAction(nameof(GetById), new { id = brand.BrandId }, ApiResponse<BrandDTO>.Ok(brand));
        }
		/// <summary>
		/// Update an existing brand.
		/// </summary>
		/// <param name="id">Brand ID</param>
		/// <param name="brandDTO">Brand update data</param>
		/// <returns>Boolean indicating success.</returns>
		[HttpPut("{id:int}")]
		[ProducesResponseType(typeof(ApiResponse<bool>), 200)]
		[ProducesResponseType(typeof(ApiResponse<string>), 400)]
		[ProducesResponseType(typeof(ApiResponse<string>), 404)]
		[ProducesResponseType(typeof(ApiResponse<string>), 500)]
		public async Task<IActionResult> Update(int id, [FromBody] BrandUpdateDTO brandDTO)
		{
			var success = await _brandService.UpdateAsync(id, brandDTO);
			if (!success)
				return NotFound(ApiResponse<string>.Fail("Brand not found or update failed"));
			return Ok(ApiResponse<bool>.Ok(true, "Brand updated successfully"));
		}
		/// <summary>
		/// Soft delete brand
		/// </summary>
		[HttpDelete("{id:int}")]
		[ProducesResponseType(typeof(ApiResponse<bool>), 200)]
		[ProducesResponseType(404)]
		public async Task<IActionResult> Delete(int id)
		{
			var success = await _brandService.DeleteAsync(id);

			if (!success)
				return NotFound(ApiResponse<bool>.Fail("Brand not found."));

			return Ok(ApiResponse<bool>.Ok(true, "Deleted successfully."));
		}
	}
}

