namespace Service.Implementations.Product
{
	public class ProductService : BaseCRUDService<DAL.Entities.Product.Product, ProductDTO, ProductCreateDTO,ProductUpdateDTO>,IProductService
	{
		

		public ProductService(IUnitOfWork unitOfWork, IMapper mapper) :base(unitOfWork, mapper) 
		{
			
			
		}
		
	}
}
