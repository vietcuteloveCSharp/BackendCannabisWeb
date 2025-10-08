namespace DTO.DTOs.PowerSupplies
{
	public class PowerSupplyCreateDTO
	{
		
		[Column(TypeName = "nvarchar(20)")]
		public EPowerSypplyType Type { get; set; }
		[Required(ErrorMessage = "Voltage is required.")]
		public int Voltage { get; set; }
		public string? Description { get; set; }
		
	}
}
