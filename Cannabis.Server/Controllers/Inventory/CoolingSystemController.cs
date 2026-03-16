using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.IServices.Inventory;
using DTO.DTOs.CoolingSystems; // Đảm bảo đúng namespace của DTO

namespace Cannabis.Server.Controllers.Inventory
{
	[ApiVersion("1.0")]
	[Route("api/v{version:apiVersion}/cooling-systems")] // Sử dụng kebab-case cho chuẩn REST
	[ApiController]
	[Authorize] // Yêu cầu xác thực (JWT) cho tất cả các endpoint
	public class CoolingSystemController(ICoolingSystemService coolingSystemService) : ControllerBase
	{
		private readonly ICoolingSystemService _coolingSystemService = coolingSystemService;

		/// <summary>
		/// Lấy danh sách toàn bộ hệ thống làm mát (bao gồm cả bản ghi đã xóa nếu cần).
		/// </summary>
		/// <response code="200">Trả về danh sách CoolingSystemDTO.</response>
		[HttpGet]
		[ProducesResponseType(typeof(ApiResponse<IEnumerable<CoolingSystemDTO>>), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetAllAsync()
		{
			var data = await _coolingSystemService.GetAllAsync();
			return Ok(ApiResponse<IEnumerable<CoolingSystemDTO>>.Ok(data));
		}

		/// <summary>
		/// Lấy danh sách các hệ thống làm mát đang hoạt động.
		/// </summary>
		/// <response code="200">Trả về danh sách CoolingSystemDTO.</response>
		[HttpGet("active")]
		[ProducesResponseType(typeof(ApiResponse<IEnumerable<CoolingSystemDTO>>), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetAllActiveAsync()
		{
			var data = await _coolingSystemService.GetAllActiveAsync();
			return Ok(ApiResponse<IEnumerable<CoolingSystemDTO>>.Ok(data));
		}

		/// <summary>
		/// Lấy thông tin chi tiết hệ thống làm mát theo ID.
		/// </summary>
		/// <param name="id">ID của hệ thống làm mát.</param>
		/// <response code="200">Trả về thông tin chi tiết.</response>
		/// <response code="404">Không tìm thấy ID yêu cầu.</response>
		[HttpGet("{id:int}")]
		[ProducesResponseType(typeof(ApiResponse<CoolingSystemDTO>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetById(int id)
		{
			var data = await _coolingSystemService.GetByIdAsync(id);
			if (data == null)
				return NotFound(ApiResponse<string>.Fail("Cooling system not found."));

			return Ok(ApiResponse<CoolingSystemDTO>.Ok(data));
		}

		/// <summary>
		/// Tạo mới một hệ thống làm mát (Yêu cầu quyền Admin).
		/// </summary>
		/// <param name="dto">Dữ liệu tạo mới.</param>
		/// <response code="201">Tạo mới thành công.</response>
		/// <response code="400">Dữ liệu không hợp lệ.</response>
		/// <response code="403">Không có quyền Admin.</response>
		[HttpPost]
		[Authorize(Roles = "Admin")]
		[ProducesResponseType(typeof(ApiResponse<CoolingSystemDTO>), StatusCodes.Status201Created)]
		[ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> Create([FromBody] CoolingSystemCreateDTO dto)
		{
			if (!ModelState.IsValid) return BadRequest(ApiResponse<string>.Fail("Invalid data."));

			var created = await _coolingSystemService.CreateAsync(dto);
			return CreatedAtAction(nameof(GetById),
				new { id = created.CoolingSystemId },
				ApiResponse<CoolingSystemDTO>.Ok(created, "Created successfully."));
		}

		/// <summary>
		/// Cập nhật thông tin hệ thống làm mát (Yêu cầu quyền Admin).
		/// </summary>
		/// <param name="id">ID hệ thống cần cập nhật.</param>
		/// <param name="dto">Dữ liệu cập nhật mới.</param>
		[HttpPut("{id:int}")]
		[Authorize(Roles = "Admin")]
		[ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Update(int id, [FromBody] CoolingSystemUpdateDTO dto)
		{
			var updated = await _coolingSystemService.UpdateAsync(id, dto);
			if (!updated)
				return NotFound(ApiResponse<string>.Fail("Cooling system not found or update failed."));

			return Ok(ApiResponse<bool>.Ok(true, "Updated successfully."));
		}

		/// <summary>
		/// Xóa mềm hệ thống làm mát khỏi danh sách hiển thị (Yêu cầu quyền Admin).
		/// </summary>
		/// <param name="id">ID hệ thống cần xóa.</param>
		[HttpDelete("{id:int}")]
		[Authorize(Roles = "Admin")]
		[ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Delete(int id)
		{
			var success = await _coolingSystemService.DeleteAsync(id);
			if (!success)
				return NotFound(ApiResponse<string>.Fail("Cooling system not found or already deleted."));

			return Ok(ApiResponse<bool>.Ok(true, "Deleted successfully."));
		}
	}
}