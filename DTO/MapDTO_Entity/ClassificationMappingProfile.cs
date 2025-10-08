using DTO.DTOs.Classifications;

namespace DTO.MapDTO_Entity
{
	public class ClassificationMappingProfile :Profile
	{
		public ClassificationMappingProfile()
		{
			#region Map Classification
			CreateMap<Classification, ClassificationDTO>(MemberList.None);
			CreateMap<ClassificationDTO, Classification>(MemberList.None);
			CreateMap<CreateClassificationDTO, Classification>(MemberList.None);
			CreateMap<UpdateClassificationDTO, UpdateClassificationDTO>(MemberList.None);
			#endregion
		}
	}
}
