namespace DTO.DTOs.ChipModels
{
	public class ChipModelDTO
	{
		public int ChipModelId { get; set; }
		public string Manufacturer { get; set; } = string.Empty;
		public string ModelChip { get; set; } = string.Empty;
		public string? Generation { get; set; }
		public decimal Efficiency { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
		public string ModelName { get; set; } = string.Empty;

		public string Description { get; set; } = string.Empty;
	}
}
