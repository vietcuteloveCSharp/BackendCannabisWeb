using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cannabis.Server.Controllers.Inventory
{
	[ApiVersion("1.0")]
	public class SpectrumController : BaseApiController<Spectrum,SpectrumDTO,SpectrumCreateDTO,SpectrumUpdateDTO>
	{
		private readonly ISpectrumService _spectrumService;

		public SpectrumController(ISpectrumService spectrumService) : base(spectrumService) 
		{
			_spectrumService = spectrumService;
		}
		
	}
}
