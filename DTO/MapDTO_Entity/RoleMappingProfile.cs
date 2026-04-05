using DAL.Entities.User;
using DTO.DTOs.Roles;

namespace DTO.MapDTO_Entity
{
	public class RoleMappingProfile :Profile
	{
		public RoleMappingProfile()
		{
			#region Map Role
			CreateMap<Role, RoleDTO>(MemberList.None).ReverseMap();
			CreateMap<RoleCreateDTO, Role>(MemberList.None)
				.ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
			CreateMap<RoleUpdateDTO, Role>(MemberList.None)
				.ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
			#endregion
		}
	}
}
