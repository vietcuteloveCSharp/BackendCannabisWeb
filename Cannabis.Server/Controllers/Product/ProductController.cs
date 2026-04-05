using DTO.DTOs.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.IServices.Product;
using Service.Services.Product;

namespace Cannabis.Server.Controllers.Product
{
	[ApiVersion("1.0")]
	[Authorize] // Tất cả người dùng đã đăng nhập đều có thể xem danh sách
	public class ProductController : BaseApiController<DAL.Entities.Product.Product,ProductDTO,ProductCreateDTO,ProductUpdateDTO>
	{
		
		public ProductController(IProductService productService) :base(productService)
		{
			
		}
		
	}
}
