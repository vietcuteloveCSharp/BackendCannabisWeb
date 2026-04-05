using DTO.DTOs.Categories;
using Service.IServices.Product;

namespace Cannabis.Server.Controllers.Product
{
	[ApiVersion("1.0")]
	
	public class CategoryController : BaseApiController<Category, CategoryDTO, CategoryCreateDTO, CategoryUpdateDTO>
	{
		public CategoryController(ICategoryService categoryService) : base(categoryService) 
		{
			
		}
		
	}
}

