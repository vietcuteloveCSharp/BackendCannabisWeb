using DTO.DTOs.Users;

namespace DTO.MapDTO_Entity
{
	public class UserMappingProfile :Profile
	{
		public UserMappingProfile()
		{
			#region Map User
			CreateMap<CreateUserDTO, User>(MemberList.None)
				.ForMember(dest => dest.HashPassword, opt => opt.Ignore())
				.ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
			CreateMap<User, UserDTO>(MemberList.None)
				.ForMember(dest => dest.Password, opt => opt.Ignore());
			CreateMap<User, UpdateUserDTO>(MemberList.None);
			CreateMap<User, UserSummaryDTO>(MemberList.None)
			  .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role != null ? src.Role.RoleName.ToString() : null));
			#endregion
		}
	}
}
