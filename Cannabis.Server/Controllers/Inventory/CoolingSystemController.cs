using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.IServices.Inventory;
using DTO.DTOs.CoolingSystems; // Đảm bảo đúng namespace của DTO

namespace Cannabis.Server.Controllers.Inventory
{
	[ApiVersion("1.0")]
	[Authorize(Roles="Admin")] // Yêu cầu xác thực (JWT) cho tất cả các endpoint
	public class CoolingSystemController : BaseApiController<CoolingSystem,CoolingSystemDTO,CoolingSystemCreateDTO, CoolingSystemUpdateDTO>
	{
		private readonly ICoolingSystemService _coolingSystemService;
		public CoolingSystemController(ICoolingSystemService coolingSystemService) :base(coolingSystemService)
		{
			_coolingSystemService = coolingSystemService;
		}

		
	}
}