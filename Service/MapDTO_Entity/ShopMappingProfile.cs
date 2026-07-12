namespace Service.MapDTO_Entity
{
	public class ShopMappingProfile:Profile
	{
		public ShopMappingProfile()
		{
			// Cấu hình chiều đọc dữ liệu (Entity -> DTO)
			CreateMap<Customer, CustomerDTO>();

			// Cấu hình chiều ghi dữ liệu đăng ký (DTO -> Entity)
			CreateMap<Shared.DTOs.DTO.Internal.RegisterRequest, Customer>()
				.ForMember(dest => dest.PasswordHash, opt => opt.Ignore()); // Password sẽ mã hóa ở AuthService/CustomerService

			// Cấu hình chiều cập nhật thông tin hồ sơ
			CreateMap<UpdateRequest, Customer>()
				.ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)); // Chỉ map các trường khác null
		}
	}
}
