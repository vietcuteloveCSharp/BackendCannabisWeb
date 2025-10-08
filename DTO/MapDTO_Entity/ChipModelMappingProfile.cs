using DTO.DTOs.ChipModels;

namespace DTO.MapDTO_Entity
{
	public class ChipModelMappingProfile :Profile
	{
		public ChipModelMappingProfile()
		{

			#region Map ChipModel
			CreateMap<ChipModel, ChipModelDTO>(MemberList.None).ReverseMap();
			CreateMap<ChipModelCreateDTO, ChipModel>(MemberList.None);
			CreateMap<ChipModelUpdateDTO, ChipModel>(MemberList.None);
			#endregion
		}
	}
}
