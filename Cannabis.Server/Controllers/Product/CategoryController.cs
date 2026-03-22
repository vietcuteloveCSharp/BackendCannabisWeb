using DTO.DTOs.Categories;
using Service.IServices.Product;

namespace Cannabis.Server.Controllers.Product
{
	[ApiVersion("1.0")]
	[Route("api/v{version:apiVersion}/[controller]")]
	[ApiController]
	public class CategoryController(ICategoryService categoryService) : ControllerBase
	{
		private readonly ICategoryService _categoryService=categoryService;
		/// <summary>
		/// Get all categories.
		/// </summary>
		/// <returns>List of categories</returns>
		[HttpGet("")]
		[ProducesResponseType(typeof(ApiResponse<IEnumerable<CategoryDTO>>), 200)]
		[ProducesResponseType(typeof(ApiResponse<CategoryDTO>),404)]
		[ProducesResponseType(typeof(ApiResponse<CategoryDTO>),500)]
		public async Task<IActionResult> GetAllAsync()
		{
			var categories = await _categoryService.GetAllAsync();
			return Ok(ApiResponse<object>.Ok(categories, "Get list of categories successfully" ));

		}

		/// <summary>
		/// Get a category by ID.
		/// </summary>
		/// <param name="id">Category ID</param>
		/// <returns>Category object</returns>
		[HttpGet("{id:int}")]
		[ProducesResponseType(typeof(ApiResponse<CategoryDTO>), 200)]
		[ProducesResponseType(typeof(ApiResponse<CategoryDTO>), 404)]
		[ProducesResponseType(typeof(ApiResponse<string>),500)]
		public async Task<IActionResult> GetById(int id)
		{
			
			var category = await _categoryService.GetByIdAsync(id);
			if (category == null)
				return NotFound(ApiResponse<CategoryDTO>.Fail("Category not found"));
			return Ok(ApiResponse<CategoryDTO>.Ok(category,"Category successfully"));
		}

		/// <summary>
		/// Create a new category.
		/// </summary>
		/// <param name="categoryDTO">Category object</param>
		/// <returns>Created category</returns>
		[HttpPost("")]
		[ProducesResponseType(typeof(ApiResponse<CategoryDTO>), 201)]
		[ProducesResponseType(typeof(ApiResponse<CategoryDTO>),400)]
		[ProducesResponseType(typeof(ApiResponse<CategoryDTO>),500)]
		public async Task<IActionResult> Create([FromBody] CategoryCreateDTO categoryDTO)
		{
			
			var createdCategory = await _categoryService.AddAsync(categoryDTO);
			if (createdCategory == null)
				return BadRequest(ApiResponse<string>.Fail("Create failed"));

			return CreatedAtAction(nameof(GetById),
				new { id = createdCategory.CategoryId },
				ApiResponse<CategoryDTO>.Ok(createdCategory, "Created successfully"));
		}

		/// <summary>
		/// Update an existing category.
		/// </summary>
		/// <param name="id">Category ID</param>
		/// <param name="category">Updated category object</param>
		/// <returns>Updated category</returns>zm    
		[HttpPut("{id:int}")]
		[ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Update(int id, [FromBody] CategoryUpdateDTO categoryDTO)
		{
			var success = await _categoryService.UpdateAsync(id, categoryDTO);
			if (!success)
				return NotFound(ApiResponse<bool>.Fail("Category not found or update failed"));
			return Ok(ApiResponse<bool>.Ok(true, "Category updated successfully"));
		}
		/// <summary>
		/// Soft delete an item by ID.
		/// </summary>
		/// <param name="id">Item ID</param>
		/// <returns>ApiResponse with boolean result</returns>
		[HttpDelete("{id:int}")]
		[ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Delete(int id)
		{
			// Gọi service xử lý xóa (thường là set IsDeleted = true và SaveChanges)
			var success = await _categoryService.DeleteAsync(id);

			if (!success)
			{
				return NotFound(ApiResponse<bool>.Fail("Item not found or already deleted."));
			}

			return Ok(ApiResponse<bool>.Ok(true, "Deleted successfully."));
		}
	}
}

