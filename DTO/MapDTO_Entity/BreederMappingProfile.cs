using DTO.DTOs.Breeders;

namespace DTO.MapDTO_Entity
{
	public class BreederMappingProfile :Profile
	{
		public BreederMappingProfile()
		{
			#region Map Breeder
				CreateMap<Breeder, BreederDTO>(MemberList.None).ReverseMap();
				CreateMap<BreederCreateDTO, Breeder>(MemberList.None);
				CreateMap<BreederUpdateDTO, Breeder>(MemberList.None);
			#endregion
			
		}
	}
}
