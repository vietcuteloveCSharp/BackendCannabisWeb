using DTO.DTOs.PowerSupplies;

namespace DTO.MapDTO_Entity
{
	public class PowerSupplyMappingProfile :Profile
	{
		public PowerSupplyMappingProfile()
		{
			#region Map PowerSupply
			CreateMap<PowerSupply, PowerSupplyDTO>(MemberList.None).ReverseMap();
			CreateMap<PowerSupplyCreateDTO, PowerSupply>(MemberList.None);
			CreateMap<PowerSupplyUpdateDTO, PowerSupply>(MemberList.None);
			#endregion
		}
	}
}
