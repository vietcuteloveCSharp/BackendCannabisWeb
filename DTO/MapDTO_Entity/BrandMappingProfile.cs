using DTO.DTOs.Brands;

namespace DTO.MapDTO_Entity
{
	public class BrandMappingProfile :Profile
	{
		public BrandMappingProfile()
		{
			#region Map Brand
			CreateMap<Brand, BrandDTO>(MemberList.None);
			CreateMap<BrandCreateDTO, Brand>(MemberList.None);
			CreateMap<BrandDTO, Brand>(MemberList.None);
			CreateMap<BrandUpdateDTO, Brand>(MemberList.None);
			#endregion
		}
	}
}
