using DTO.DTOs.Admin.Admins;
using DTO.DTOs.User.Users;

namespace DTO.MapDTO_Entity
{
	public class UserMappingProfile :Profile
	{
		public UserMappingProfile()
		{
			#region Map User
			CreateMap<CreateUserDTO, User>(MemberList.Source)
				.ForMember(dest => dest.HashPassword, opt => opt.Ignore())
				.ForMember(dest => dest.RoleId, opt => opt.Ignore())
				.ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
			CreateMap<adminCreateDTO, User>(MemberList.Source)
				.ForMember(dest => dest.HashPassword, opt => opt.Ignore())
				.ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
				.ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
			CreateMap<User, UserDTO>(MemberList.Source)
				.ForMember(dest => dest.Password, opt => opt.Ignore());
			CreateMap<User, UpdateUserDTO>(MemberList.Source);
			CreateMap<UpdateUserDTO, User>().ReverseMap()
				.ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null)); ;
			CreateMap<User, UserSummaryDTO>(MemberList.Source)
			  .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role != null ? src.Role.RoleName.ToString() : null));
			#endregion
		}
	}
}
