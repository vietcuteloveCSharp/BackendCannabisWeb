using DTO.DTOs.Addresses;

namespace DTO.MapDTO_Entity
{
	public class AddressMappingProfile :Profile
	{
		public AddressMappingProfile()
		{
			#region Map Address	
			CreateMap<AddressCreateDTO, Address>();
			CreateMap<AddressUpdateDTO, Address>();
			CreateMap<Address, AddressDTO>().ReverseMap();
				
			#endregion
		}
	}
}
