

namespace Cannabis.Server.Controllers.Product
{
	[ApiVersion("1.0")]
	
	public class CategoryController : BaseCrudController<Category, CategoryDTO, CategoryCreateDTO, CategoryUpdateDTO>
	{
		public CategoryController(ICategoryService categoryService) : base(categoryService) 
		{
			
		}
		
	}
}

