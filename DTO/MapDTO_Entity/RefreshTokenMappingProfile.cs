namespace DTO.MapDTO_Entity
{
	public class RefreshTokenMappingProfile :Profile
	{
		public RefreshTokenMappingProfile()
		{
			#region Map RefreshToken
			CreateMap<RefreshTokenDTO, RefreshToken>(MemberList.None).ReverseMap();
			CreateMap<RefreshTokenCreateDTO, RefreshToken>(MemberList.None);
			#endregion
		}
	}
}
