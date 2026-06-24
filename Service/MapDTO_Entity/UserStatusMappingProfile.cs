namespace Service.MapDTO_Entity
{
	public class UserStatusMappingProfile :Profile
	{
		public UserStatusMappingProfile()
		{
			CreateMap<UserStatusDTO,UserStatus>().ReverseMap();
			CreateMap<UserStatusCreateDTO, UserStatus>();
			CreateMap<UserStatusUpdateDTO,UserStatus >();
		}
	}
}
