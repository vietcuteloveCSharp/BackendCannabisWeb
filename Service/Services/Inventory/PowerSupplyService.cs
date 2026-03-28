using Service.IServices.Inventory;
using Service.Services.BaseService;

namespace Service.Services.Inventory
{
	public class PowerSupplyService : BaseService<PowerSupply, PowerSupplyDTO, PowerSupplyCreateDTO, PowerSupplyUpdateDTO>, IPowerSupplyService
	{	
		public PowerSupplyService(IUnitOfWork unitOfWork, IMapper mapper) :base(unitOfWork,mapper)
		{
			
		}
		
	}
}
