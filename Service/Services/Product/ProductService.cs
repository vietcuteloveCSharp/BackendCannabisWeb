using AutoMapper.QueryableExtensions;
using DTO.DTOs.Products;

namespace Service.Services.Product
{
	public class ProductService : BaseService<DAL.Entities.Product.Product, ProductDTO, ProductCreateDTO,ProductUpdateDTO>,IProductService
	{
		

		public ProductService(IUnitOfWork unitOfWork, IMapper mapper) :base(unitOfWork, mapper) 
		{
			
			
		}
		
	}
}
