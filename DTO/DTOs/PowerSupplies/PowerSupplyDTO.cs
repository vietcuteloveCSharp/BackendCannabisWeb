namespace DTO.DTOs.PowerSupplies
{
	public class PowerSupplyDTO
	{
		public int Id { get; set; }
		public EPowerSypplyType Type { get; set; }
		public int Voltage { get; set; }
		public string? Description { get; set; }
		public DateTime CreateAt { get; set; } 
		public DateTime UpdateAt { get; set; }
	}
}
