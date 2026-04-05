using DAL.Entities.User;

namespace DTO.MapDTO_Entity
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
