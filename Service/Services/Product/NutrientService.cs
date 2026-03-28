using Service.IServices.Product;
using Service.Services.BaseService;

namespace Service.Services.Product
{
	public class NutrientService : BaseService<Nutrient, NutrientDTO, NutrientCreateDTO, NutrientUpdateDTO>,INutrientService
	{
		
		public NutrientService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) 
		{
			
		}
		
	}
}
