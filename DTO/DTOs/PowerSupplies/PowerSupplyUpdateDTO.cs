namespace DTO.DTOs.PowerSupplies
{
	public class PowerSupplyUpdateDTO
	{
		[Column(TypeName = "nvarchar(20)")]
		public EPowerSypplyType Type { get; set; }
		public int Voltage { get; set; }
		public string? Description { get; set; }
		
	}
}
