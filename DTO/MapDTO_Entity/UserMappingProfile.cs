using DAL.Entities.User;
using DTO.DTOs.Admin.Admins;
using DTO.DTOs.User.Users;

namespace DTO.MapDTO_Entity
{
	public class UserMappingProfile :Profile
	{
		public UserMappingProfile()
		{
			#region Map User

			// 1. Map từ DTO đăng ký User thường sang Entity
			CreateMap<CreateUserDTO, User>(MemberList.Source)
				.ForMember(dest => dest.PasswordHash, opt => opt.Ignore()) // Xử lý băm mật khẩu ở Service
				.ForMember(dest => dest.RoleId, opt => opt.Ignore())     // Gán Role mặc định ở Service
				.ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

			// 2. Map từ DTO tạo Admin sang Entity
			CreateMap<AdminCreateDTO, User>(MemberList.Source)
				.ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
				 
			

			// 3. Map từ Entity sang UserDTO (Dùng cho Response API)
			CreateMap<User, UserDTO>(MemberList.Source)
				.ForMember(dest => dest.Password, opt => opt.Ignore()) // Không trả về mật khẩu
				// Chuyển Enum Role sang String để hiển thị tên quyền hạn
				.ForMember(dest => dest.RoleName, opt => opt.MapFrom(src =>
					src.Role != null ? src.Role.RoleName.ToString() : string.Empty))
				// Chuyển Enum Status sang String (Active, Blocked...)
				.ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

			// 4. Map cho các trường hợp Update
			CreateMap<User, UpdateUserDTO>(MemberList.Source);
			CreateMap<UpdateUserDTO, User>().ReverseMap()
				.ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

			// 5. Map thu gọn cho danh sách hoặc Search
			CreateMap<User, UserSummaryDTO>(MemberList.Source)
				.ForMember(dest => dest.RoleName, opt => opt.MapFrom(src =>
					src.Role != null ? src.Role.RoleName.ToString() : null));

			#endregion
		}
	}
}
