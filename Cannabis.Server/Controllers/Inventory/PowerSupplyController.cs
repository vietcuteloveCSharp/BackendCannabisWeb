using DAL.Entities;
using DTO.DTOs.PowerSupplies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.IServices.Inventory;

namespace Cannabis.Server.Controllers.Inventory
{
	[ApiVersion("1.0")]
	[Authorize]
	public class PowerSupplyController : BaseApiController<PowerSupply,PowerSupplyDTO,PowerSupplyCreateDTO,PowerSupplyUpdateDTO>
	{
		private readonly IPowerSupplyService _powerSupplyService;
		public PowerSupplyController(IPowerSupplyService powerSupplyService) :base(powerSupplyService)
		{
			this._powerSupplyService = powerSupplyService;
		}
	
	}
}
