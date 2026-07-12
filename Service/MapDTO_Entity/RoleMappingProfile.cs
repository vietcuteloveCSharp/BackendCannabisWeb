namespace Service.MapDTO_Entity
{
	public class RoleMappingProfile : Profile
	{
		public RoleMappingProfile()
		{
			#region Map Role
			CreateMap<DAL.Entities.Internal.Role, RoleDTO>(MemberList.None).ReverseMap();
			CreateMap<RoleCreateDTO, DAL.Entities.Internal.Role>(MemberList.None);
			CreateMap<RoleUpdateDTO, DAL.Entities.Internal.Role>(MemberList.None);
			#endregion
		}
	}
}
