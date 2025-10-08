using DTO.DTOs.NutrientTypes;

namespace DTO.MapDTO_Entity
{
	public class NutrientTypeMappingProfile :Profile
	{
		public NutrientTypeMappingProfile()
		{
			#region Map NutrientType
			CreateMap<NutrientType, NutrientTypeDTO>(MemberList.None);
			CreateMap<NutrientTypeDTO, NutrientType>(MemberList.None);
			CreateMap<NutrientTypeCreateDTO, NutrientType>(MemberList.None);
			CreateMap<NutrientTypeUpdateDTO, NutrientType>(MemberList.None);
			#endregion
		}
	}
}
