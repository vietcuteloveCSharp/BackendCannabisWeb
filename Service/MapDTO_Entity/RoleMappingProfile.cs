

namespace Service.MapDTO_Entity
{
	public class RoleMappingProfile :Profile
	{
		public RoleMappingProfile()
		{
			#region Map Role
			CreateMap<DAL.Entities.User.Role, RoleDTO>(MemberList.None).ReverseMap();
			CreateMap<RoleCreateDTO, DAL.Entities.User.Role>(MemberList.None)
				.ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
			CreateMap<RoleUpdateDTO, DAL.Entities.User.Role>(MemberList.None)
				.ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
			#endregion
		}
	}
}
