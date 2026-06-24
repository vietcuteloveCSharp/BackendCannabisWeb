namespace Service.Implementations.Product
{
	public class BrandService : BaseCRUDService<Brand,BrandDTO,BrandCreateDTO,BrandUpdateDTO>, IBrandService
	{
		
		public BrandService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) 
		{
			
		}
		
	}
}
