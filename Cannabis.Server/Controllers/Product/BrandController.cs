




namespace Cannabis.Server.Controllers.Product
{
	[ApiVersion("1.0")]

	public class BrandController: BaseCrudController<Brand,BrandDTO,BrandCreateDTO,BrandUpdateDTO>
	{
		private readonly IBrandService _brandService;
		public BrandController(IBrandService brandService) :base(brandService)
		{
			_brandService = brandService;
		}
		
	}
}

