using Service.Services.BaseService;
using DTO.DTOs.CoolingSystems;
using DTO.Response;
using Service.IServices.Inventory;

namespace Service.Services.Inventory
{
	public class CoolingSystemService : BaseService<CoolingSystem, CoolingSystemDTO, CoolingSystemCreateDTO, CoolingSystemUpdateDTO>,ICoolingSystemService
	{
		
		public CoolingSystemService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork,mapper) 
		{
			
		}
		
	
		
	
	}
}
