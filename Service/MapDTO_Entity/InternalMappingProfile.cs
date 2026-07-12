
namespace Service.MapDTO_Entity
{
	public class InternalMappingProfile : Profile
	{
		public InternalMappingProfile()
		{
			CreateMap<Staff, StaffDTO>()
				.ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role != null ? src.Role.RoleName : string.Empty))
				.ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status != null ? src.Status.Name : string.Empty));

			CreateMap<Staff, StaffSummaryDTO>()
				.ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role != null ? src.Role.RoleName : string.Empty));

			// --- CHIỀU GHI DỮ LIỆU (Requests -> Entity) ---
			CreateMap<Shared.DTOs.DTO.Internal.RegisterRequest, Staff>()
				.ForMember(dest => dest.PasswordHash, opt => opt.Ignore()); // Sẽ hash thủ công bằng IPasswordHasher
		}
	}
}
