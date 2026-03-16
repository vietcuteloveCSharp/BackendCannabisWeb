using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.IServices.Product;
using DTO.DTOs.Classifications; // Đảm bảo đúng namespace

namespace Cannabis.Server.Controllers.Product
{
	[ApiVersion("1.0")]
	[Route("api/v{version:apiVersion}/classifications")]
	[ApiController]
	[Authorize] // Yêu cầu xác thực cơ bản để xem
	public class ClassificationController(IClassificationService classificationService) : ControllerBase
	{
		private readonly IClassificationService _classificationService = classificationService;

		/// <summary>
		/// Lấy toàn bộ danh sách phân loại hạt giống.
		/// </summary>
		[HttpGet]
		[ProducesResponseType(typeof(ApiResponse<IEnumerable<ClassificationDTO>>), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetAllAsync()
		{
			var result = await _classificationService.GetAllAsync();
			return Ok(ApiResponse<IEnumerable<ClassificationDTO>>.Ok(result));
		}

		/// <summary>
		/// Lấy thông tin phân loại theo ID.
		/// </summary>
		/// <param name="id">ID của phân loại.</param>
		[HttpGet("{id:int}")]
		[ProducesResponseType(typeof(ApiResponse<ClassificationDTO>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetByIdAsync(int id)
		{
			var result = await _classificationService.GetByIdAsync(id);
			if (result == null)
				return NotFound(ApiResponse<string>.Fail("Classification not found."));

			return Ok(ApiResponse<ClassificationDTO>.Ok(result));
		}

		/// <summary>
		/// Tạo mới phân loại (Quyền Admin).
		/// </summary>
		[HttpPost]
		[Authorize(Roles = "Admin")]
		[ProducesResponseType(typeof(ApiResponse<ClassificationDTO>), StatusCodes.Status201Created)]
		[ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> CreateAsync([FromBody] ClassificationCreateDTO dto)
		{
			if (!ModelState.IsValid) return BadRequest(ApiResponse<string>.Fail("Invalid data."));

			var created = await _classificationService.CreateAsync(dto);
			if (created == null) return BadRequest(ApiResponse<string>.Fail("Creation failed."));

			return CreatedAtAction(
				nameof(GetByIdAsync),
				new { id = created.ClassificationId },
				ApiResponse<ClassificationDTO>.Ok(created, "Classification created successfully.")
			);
		}

		/// <summary>
		/// Cập nhật phân loại (Quyền Admin).
		/// </summary>
		[HttpPut("{id:int}")]
		[Authorize(Roles = "Admin")]
		[ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
		public async Task<IActionResult> UpdateAsync(int id, [FromBody] ClassificationUpdateDTO dto)
		{
			var updated = await _classificationService.UpdateAsync(id, dto);
			if (!updated)
				return NotFound(ApiResponse<string>.Fail("Classification not found or update failed."));

			return Ok(ApiResponse<bool>.Ok(true, "Classification updated successfully."));
		}

		/// <summary>
		/// Xóa mềm phân loại (Quyền Admin).
		/// </summary>
		[HttpDelete("{id:int}")]
		[Authorize(Roles = "Admin")]
		[ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
		public async Task<IActionResult> DeleteAsync(int id)
		{
			var deleted = await _classificationService.DeleteAsync(id);
			if (deleted)
				return NotFound(ApiResponse<string>.Fail("Classification not found or already deleted."));

			return Ok(ApiResponse<bool>.Ok(true, "Classification deleted successfully."));
		}
	}
}