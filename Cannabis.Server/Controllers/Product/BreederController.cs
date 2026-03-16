using DTO.DTOs.Breeders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.IServices.Product;

namespace Cannabis.Server.Controllers.Product
{
	[ApiVersion("1.0")]
	[Route("api/v{version:apiVersion}/breeders")]
	[ApiController]
	[Authorize] // Yêu cầu đăng nhập cho tất cả các thao tác xem
	public class BreederController(IBreederService breederService) : ControllerBase
	{
		private readonly IBreederService _breederService = breederService;

		/// <summary>
		/// Lấy danh sách toàn bộ nhà nhân giống.
		/// </summary>
		/// <response code="200">Trả về danh sách BreederDTO.</response>
		/// <response code="401">Chưa xác thực (Unauthorized).</response>
		[HttpGet]
		[Authorize(Roles = "Admin")]
		[ProducesResponseType(typeof(ApiResponse<IEnumerable<BreederDTO>>), StatusCodes.Status200OK)]
		[ProducesResponseType(404)]
		public async Task<IActionResult> GetAll()
		{
			var breeders = await _breederService.GetAllAsync();
			return Ok(ApiResponse<IEnumerable<BreederDTO>>.Ok(breeders, "Get all breeders successfully."));
		}
		/// <summary>
		/// Lấy danh sách toàn bộ nhà nhân giống active.
		/// </summary>
		[HttpGet("active")]
		[ProducesResponseType(typeof(ApiResponse<IEnumerable<BreederDTO>>), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetAllActive()
		{
			var breeders = await _breederService.GetAllActiveAsync();
			return Ok(ApiResponse<IEnumerable<BreederDTO>>.Ok(breeders, "Get all breeders successfully."));
		}

		/// <summary>
		/// Lấy thông tin chi tiết nhà nhân giống theo ID.
		/// </summary>
		/// <param name="id">ID của nhà nhân giống.</param>
		/// <response code="200">Trả về thông tin chi tiết nhà nhân giống.</response>
		/// <response code="404">Không tìm thấy nhà nhân giống.</response>
		[HttpGet("{id:int}")]
		[ProducesResponseType(typeof(ApiResponse<BreederDTO>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetById(int id)
		{
			var breeder = await _breederService.GetByIdAsync(id);
			if (breeder == null)
				return NotFound(ApiResponse<string>.Fail("Breeder not found."));

			return Ok(ApiResponse<BreederDTO>.Ok(breeder));
		}

		/// <summary>
		/// Thêm mới một nhà nhân giống (Chỉ Admin).
		/// </summary>
		/// <param name="breederCreateDTO">Thông tin tạo mới nhà nhân giống.</param>
		/// <response code="201">Tạo mới thành công.</response>
		/// <response code="400">Dữ liệu đầu vào không hợp lệ.</response>
		/// <response code="403">Không có quyền thực hiện (Forbidden).</response>
		[HttpPost]
		[Authorize(Roles = "Admin")]
		[ProducesResponseType(typeof(ApiResponse<BreederDTO>), StatusCodes.Status201Created)]
		[ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> Create([FromBody] BreederCreateDTO breederCreateDTO)
		{
			if (!ModelState.IsValid) return BadRequest(ApiResponse<string>.Fail("Invalid model data."));

			var created = await _breederService.AddAsync(breederCreateDTO);
			if (created == null) return BadRequest(ApiResponse<string>.Fail("Failed to create breeder."));

			return CreatedAtAction(nameof(GetById),
				new { id = created.BreederId },
				ApiResponse<BreederDTO>.Ok(created, "Breeder created successfully."));
		}

		/// <summary>
		/// Cập nhật thông tin nhà nhân giống (Chỉ Admin).
		/// </summary>
		/// <param name="id">ID nhà nhân giống cần sửa.</param>
		/// <param name="breederUpdateDTO">Dữ liệu cập nhật.</param>
		[HttpPut("{id:int}")]
		[Authorize(Roles = "Admin")]
		[ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Update(int id, [FromBody] BreederUpdateDTO breederUpdateDTO)
		{
			var result = await _breederService.UpdateAsync(id, breederUpdateDTO);
			if (!result)
				return NotFound(ApiResponse<string>.Fail("Breeder not found or update failed."));

			return Ok(ApiResponse<bool>.Ok(true, "Breeder updated successfully."));
		}

		/// <summary>
		/// Xóa mềm nhà nhân giống (Chỉ Admin).
		/// </summary>
		/// <param name="id">ID nhà nhân giống cần xóa.</param>
		[HttpDelete("{id:int}")]
		[Authorize(Roles = "Admin")]
		[ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Delete(int id)
		{
			// Lưu ý: Đổi tên hàm service cho khớp với các bảng khác (DeleteAsync)
			var result = await _breederService.DeleteAsync(id);

			if (!result)
				return NotFound(ApiResponse<bool>.Fail("Breeder not found."));

			return Ok(ApiResponse<bool>.Ok(true, "Breeder deleted successfully."));
		}
	}
}