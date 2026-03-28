using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.IServices.Inventory;
using Service.Services.Inventory;


namespace Cannabis.Server.Controllers.Inventory
{
	[ApiVersion("1.0")]
	[Authorize]
	public class NutrientTypeController : BaseApiController<NutrientType,NutrientTypeDTO,NutrientTypeCreateDTO,NutrientTypeUpdateDTO>
	{
		private readonly INutrientTypeService _nutrientTypeServic;
		public NutrientTypeController(INutrientTypeService nutrientTypeService) : base(nutrientTypeService)
		{
				_nutrientTypeServic = nutrientTypeService;
		}

	}
}
