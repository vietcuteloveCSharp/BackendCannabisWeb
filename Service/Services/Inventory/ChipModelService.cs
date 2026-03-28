using DTO.DTOs.ChipModels;
using Service.IServices.Inventory;
using Service.Services.BaseService;

namespace Service.Services.Inventory
{
	public class ChipModelService : BaseService<ChipModel, ChipModelDTO,ChipModelCreateDTO,ChipModelUpdateDTO>,IChipModelService
	{
		
		public ChipModelService(IUnitOfWork unitOfWork, IMapper mapper) :base(unitOfWork,mapper)
		{
			
		}

	}
}
