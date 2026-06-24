


namespace Cannabis.Server.Controllers.Product
{
	[ApiVersion("1.0")]
	[Authorize] // Tất cả người dùng đã đăng nhập đều có thể xem danh sách
	public class ProductController : BaseCrudController<DAL.Entities.Product.Product,ProductDTO,ProductCreateDTO,ProductUpdateDTO>
	{
		
		public ProductController(IProductService productService) :base(productService)
		{
			
		}
		
	}
}
