using DTO.DTOs.CarbonFilters;

namespace DTO.MapDTO_Entity
{
	public class CarbonFilerMappingProfile :Profile
	{
		public CarbonFilerMappingProfile()
		{
			#region Map CarbonFiler
			CreateMap<CarbonFilter, CarbonFilterDTO>(MemberList.None);
			CreateMap<CarbonFilterDTO, CarbonFilter>(MemberList.None);
			CreateMap<CarbonFilterCreateDTO, CarbonFilter>(MemberList.None);
			CreateMap<CarbonFilterUpdateDTO, CarbonFilter>(MemberList.None);
			#endregion
		}
	}
}
