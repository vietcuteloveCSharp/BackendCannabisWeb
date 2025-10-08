using DTO.DTOs.CoolingSystems;

namespace DTO.MapDTO_Entity
{
	public class CoolingSystemMappingProfile:Profile
	{
		public CoolingSystemMappingProfile()
		{
			#region Map CoolingSystem
			CreateMap<CoolingSystemCreateDTO, CoolingSystem>(MemberList.None);
			CreateMap<CoolingSystemUpdateDTO, CoolingSystem>(MemberList.None);
			CreateMap<CoolingSystem, CoolingSystemDTO>(MemberList.None)
				.ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()));
			#endregion
		}
	}
}
