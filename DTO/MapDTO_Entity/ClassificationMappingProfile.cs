using DTO.DTOs.Classifications;

namespace DTO.MapDTO_Entity
{
	public class ClassificationMappingProfile :Profile
	{
		public ClassificationMappingProfile()
		{
			#region Map Classification
			CreateMap<Classification, ClassificationDTO>(MemberList.None).ReverseMap();
			CreateMap<ClassificationCreateDTO, Classification>(MemberList.None);
			CreateMap<ClassificationUpdateDTO, ClassificationUpdateDTO>(MemberList.None);
			#endregion
		}
	}
}
