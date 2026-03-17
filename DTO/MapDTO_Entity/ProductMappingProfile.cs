using DTO.DTOs.Products;
using DTO.DTOs.Seeds;
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

			// Map từ Mega DTO sang Product
			CreateMap<SeedCreateRequestDTO, Product>()
				.ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => "Seed")) // Fix cứng loại
				.ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))     // Mặc định Active
				.ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now));

			// ProductUpdateDto → Product
			CreateMap<ProductUpdateDTO, Product>()
				.ForMember(dest => dest.BrandId, opt => opt.Ignore())
				.ForMember(dest => dest.ProductType, opt => opt.Ignore());
		}
	}
}
