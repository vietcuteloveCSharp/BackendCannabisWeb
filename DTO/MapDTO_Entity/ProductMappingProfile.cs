using DAL.Entities.Product;
using DTO.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.MapDTO_Entity
{
	public class ProductMappingProfile :Profile
	{
		public ProductMappingProfile()
		{
			// Product → ProductResponseDto
			CreateMap<Product, ProductDTO>().
			ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category!.CategoryName))
			.ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand != null ? src.Brand.BrandName : null));

			// ProductCreateDto → Product
			CreateMap<ProductCreateDTO, Product>();



			// ProductUpdateDto → Product
			CreateMap<ProductUpdateDTO, Product>()
				.ForMember(dest => dest.BrandId, opt => opt.Ignore());
				
		}
	}
}
