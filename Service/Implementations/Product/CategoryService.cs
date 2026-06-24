namespace Service.Implementations.Product
{
	public class CategoryService : BaseCRUDService<Category, CategoryDTO,CategoryCreateDTO,CategoryUpdateDTO>, ICategoryService
	{	
		public CategoryService(IUnitOfWork unitOfWork, IMapper mapper) :base(unitOfWork, mapper) 
		{ 
			
		}
	
	}
}
