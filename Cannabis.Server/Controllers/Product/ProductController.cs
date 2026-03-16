using DTO.DTOs.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.IServices.Product;
using Service.Services.Product;

namespace Cannabis.Server.Controllers.Product
{
	[ApiVersion("1.0")]
	[Route("api/v{version:apiVersion}/products")]
	[ApiController]
	[Authorize] // Tất cả người dùng đã đăng nhập đều có thể xem danh sách
	public class ProductController : ControllerBase
	{
		private readonly IProductService _productService;
		public ProductController(IProductService productService)
		{
			this._productService = productService;
		}
		/// <summary>
		/// Lấy toàn bộ danh sách sản phẩm (Bao gồm tên Category và Brand).
		/// </summary>
		/// <response code="200">Trả về danh sách ProductDTO.</response>
		[HttpGet]
		[ProducesResponseType(typeof(ApiResponse<IEnumerable<ProductDTO>>), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetAll()
		{
			var products = await _productService.GetAllAsync();
			return Ok(ApiResponse<IEnumerable<ProductDTO>>.Ok(products));
		}

		/// <summary>
		/// Lấy danh sách sản phẩm đang hoạt động kinh doanh.
		/// </summary>
		[HttpGet("active")]
		[ProducesResponseType(typeof(ApiResponse<IEnumerable<ProductDTO>>), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetAllActive()
		{
			var products = await _productService.GetAllActiveAsync();
			return Ok(ApiResponse<IEnumerable<ProductDTO>>.Ok(products));
		}

		/// <summary>
		/// Lấy chi tiết một sản phẩm theo ID.
		/// </summary>
		/// <param name="id">ID sản phẩm.</param>
		[HttpGet("{id:int}")]
		[ProducesResponseType(typeof(ApiResponse<ProductDTO>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetById(int id)
		{
			var product = await _productService.GetByIdAsync(id);
			if (product == null)
				return NotFound(ApiResponse<string>.Fail("Product not found."));

			return Ok(ApiResponse<ProductDTO>.Ok(product));
		}

		/// <summary>
		/// Tạo mới sản phẩm (Chỉ Admin).
		/// </summary>
		/// <remarks>
		/// Dữ liệu trả về sẽ bao gồm đầy đủ CategoryName và BrandName để Frontend hiển thị ngay.
		/// </remarks>
		[HttpPost]
		[Authorize(Roles = "Admin")]
		[ProducesResponseType(typeof(ApiResponse<ProductDTO>), StatusCodes.Status201Created)]
		[ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> Create([FromBody] ProductCreateDTO dto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ApiResponse<string>.Fail("Invalid data."));

			var result = await _productService.CreateAsync(dto);
			return CreatedAtAction(nameof(GetById), new { id = result.ProductId },
				ApiResponse<ProductDTO>.Ok(result, "Product created successfully."));
		}

		/// <summary>
		/// Cập nhật thông tin cơ bản sản phẩm (Chỉ Admin).
		/// </summary>
		[HttpPut("{id:int}")]
		[Authorize(Roles = "Admin")]
		[ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Update(int id, [FromBody] ProductUpdateDTO dto)
		{
			var success = await _productService.UpdateAsync(id, dto);
			if (!success)
				return NotFound(ApiResponse<string>.Fail("Product not found or update failed."));

			return Ok(ApiResponse<bool>.Ok(true, "Product updated successfully."));
		}

		/// <summary>
		/// Bật/Tắt trạng thái hoạt động của sản phẩm (Chỉ Admin).
		/// </summary>
		[HttpPatch("{id:int}/toggle-active")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> ToggleActive(int id, [FromQuery] bool isActive)
		{
			var result = await _productService.ToggleActiveAsync(id, isActive);
			if (!result) return NotFound(ApiResponse<string>.Fail("Product not found."));

			return Ok(ApiResponse<bool>.Ok(true, $"Product status changed to {(isActive ? "Active" : "Inactive")}."));
		}

		/// <summary>
		/// Xóa mềm sản phẩm (Chỉ Admin).
		/// </summary>
		[HttpDelete("{id:int}")]
		[Authorize(Roles = "Admin")]
		[ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Delete(int id)
		{
			var result = await _productService.DeleteAsync(id);
			if (!result)
				return NotFound(ApiResponse<string>.Fail("Product not found or already deleted."));

			return Ok(ApiResponse<bool>.Ok(true, "Product deleted successfully."));
		}
	}
}
