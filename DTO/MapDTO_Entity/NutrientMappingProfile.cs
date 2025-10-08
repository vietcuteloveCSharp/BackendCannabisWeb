using DTO.DTOs.Nutrients;

namespace DTO.MapDTO_Entity
{
	public class NutrientMappingProfile :Profile
	{
		public NutrientMappingProfile()
		{
			#region Map Nutrient
			CreateMap<NutrientCreateDTO, Nutrient>();
			CreateMap<NutrientUpdateDTO, Nutrient>();
			CreateMap<Nutrient, NutrientDTO>()
				.ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand != null ? src.Brand.BrandName : null))
				.ForMember(dest => dest.NutrientTypeName, opt => opt.MapFrom(src => src.NutrientType != null ? src.NutrientType.NutrientName : null));
			#endregion
		}
	}
}
