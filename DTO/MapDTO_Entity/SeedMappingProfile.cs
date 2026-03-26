using DTO.DTOs.Seeds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.MapDTO_Entity
{
	public class SeedMappingProfile :Profile
	{
		public SeedMappingProfile()
		{
			CreateMap<Seed, SeedDTO>()
			.ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product!.ProductName))
			.ForMember(dest => dest.BreederName, opt => opt.MapFrom(src => src.Breeder!.BreederName));
			

			// Map từ Mega DTO sang Seed
			CreateMap<SeedCreateRequestDTO, Seed>()
				.ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now));
		}
	}
}
