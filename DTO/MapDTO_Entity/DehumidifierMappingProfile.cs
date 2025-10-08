using DTO.DTOs.Dehumidifiers;

namespace DTO.MapDTO_Entity
{
	public class DehumidifierMappingProfile :Profile
	{
		public DehumidifierMappingProfile()
		{
			CreateMap<Dehumidifier, DehumidifierDTO>(MemberList.None).ReverseMap();
			CreateMap<DehumidifierCreateDTO, Dehumidifier>(MemberList.None);
			CreateMap<DehumidifierUpdateDTO, Dehumidifier>(MemberList.None);

		}
	}
}
