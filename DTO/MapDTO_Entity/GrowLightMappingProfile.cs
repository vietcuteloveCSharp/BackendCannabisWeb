using DTO.DTOs.GrowLights;

namespace DTO.MapDTO_Entity
{
	public class GrowLightMappingProfile :Profile
	{
		public GrowLightMappingProfile()
		{
			#region Map GrowLight
			CreateMap<GrowLight, GrowLightDTO>(MemberList.None).ReverseMap();
			CreateMap<GrowLightCreateDTO, GrowLight>(MemberList.None);
			CreateMap<GrowLightUpdateDTO, GrowLight>(MemberList.None);
			#endregion
		}
	}
}
