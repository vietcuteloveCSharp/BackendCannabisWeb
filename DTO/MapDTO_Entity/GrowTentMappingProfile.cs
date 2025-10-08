using DTO.DTOs.GrowTents;

namespace DTO.MapDTO_Entity
{
	public class GrowTentMappingProfile :Profile
	{
		public GrowTentMappingProfile()
		{
			#region Map GrowTent
			CreateMap<GrowTent, GrowTentDTO>(MemberList.None);
			CreateMap<GrowTentDTO, GrowTent>(MemberList.None);
			CreateMap<GrowTentCreateDTO, GrowTent>(MemberList.None);
			CreateMap<GrowTentUpdateDTO, GrowTent>(MemberList.None);
			#endregion
		}
	}
}
