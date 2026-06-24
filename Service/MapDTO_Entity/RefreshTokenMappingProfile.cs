namespace Service.MapDTO_Entity
{
	public class RefreshTokenMappingProfile :Profile
	{
		public RefreshTokenMappingProfile()
		{
			#region Map RefreshToken
			CreateMap<RefreshTokenDTO, UserRefreshToken>(MemberList.None).ReverseMap();
			CreateMap<RefreshTokenCreateDTO, UserRefreshToken>(MemberList.None);
			#endregion
		}
	}
}
