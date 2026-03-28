using Service.IServices.Inventory;
using Service.Services.BaseService;

namespace Service.Services.Inventory
{
	public class NutrientTypeService : BaseService<NutrientType, NutrientTypeDTO, NutrientTypeUpdateDTO, NutrientTypeCreateDTO>, INutrientTypeService
	{
		
		public NutrientTypeService(IUnitOfWork unitOfWork, IMapper mapper) :base(unitOfWork,mapper) 
		{

		}
		

		public async Task<bool> NameExists(string name)
		{
			var nutrient = await _unitOfWork.NutrientTypes.AnyAsync(
				x => x.NutrientName.ToLower() == name.ToLower());
			return  true;
		}


		
	}
}
