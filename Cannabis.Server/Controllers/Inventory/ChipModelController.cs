using DTO.DTOs.ChipModels;
using Service.IServices.Inventory;

namespace Cannabis.Server.Controllers.Inventory
{
	[ApiVersion("1.0")]
	[Authorize(Roles ="Admin")]
	public class ChipModelController : BaseApiController<ChipModel, ChipModelDTO,ChipModelCreateDTO,ChipModelUpdateDTO>
	{
		private readonly IChipModelService _chipModelService;
		public ChipModelController(IChipModelService chipModelService) :base(chipModelService)
		{
			this._chipModelService = chipModelService;
		}
		
		
	}
}
