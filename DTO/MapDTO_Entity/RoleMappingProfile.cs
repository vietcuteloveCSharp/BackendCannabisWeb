using DTO.DTOs.Roles;

namespace DTO.MapDTO_Entity
{
	public class RoleMappingProfile :Profile
	{
		public RoleMappingProfile()
		{
			#region Map Role
			CreateMap<Role, RoleDTO>(MemberList.None);
			CreateMap<RoleDTO, Role>(MemberList.None);
			CreateMap<CreateRoleDTO, Role>(MemberList.None)
				.ForMember(dest => dest.UpdatedAt, opt => opt.Ignore()); 
			CreateMap<RoleUpdateDTO, Role>(MemberList.None);
			#endregion
		}
	}
}
