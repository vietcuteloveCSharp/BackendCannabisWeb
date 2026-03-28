namespace DTO.DTOs.CoolingSystems
{
	public class CoolingSystemDTO
	{
		public int Id { get; set; }
		public ECoolingType Type { get; set; }
		public string? Description { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
	}
}
