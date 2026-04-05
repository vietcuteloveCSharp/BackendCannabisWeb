

namespace Service.Services.Product
{
	public class BrandService : BaseService<Brand,BrandDTO,BrandCreateDTO,BrandUpdateDTO>, IBrandService
	{
		
		public BrandService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) 
		{
			
		}
		
	}
}
