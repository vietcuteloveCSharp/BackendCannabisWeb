using Service.IServices.Product;
using Service.Services.BaseService;

namespace Service.Services.Product
{
	public class CategoryService : BaseService<Category, CategoryDTO,CategoryCreateDTO,CategoryUpdateDTO>, ICategoryService
	{	
		public CategoryService(IUnitOfWork unitOfWork, IMapper mapper) :base(unitOfWork, mapper) 
		{ 
			
		}
	
	}
}
