namespace DTO.DTOs.ChipModels
{
	public class ChipModelCreateDTO
	{
		[StringLength(100, ErrorMessage = "Manufacturer must not exceed 100 characters.")]
		public string Manufacturer { get; set; } = string.Empty;

		[StringLength(100, ErrorMessage = "ModelChip must not exceed 100 characters.")]
		public string ModelChip { get; set; } = string.Empty;

		[StringLength(50, ErrorMessage = "Generation must not exceed 50 characters.")]
		public string? Generation { get; set; }

		[Range(0, 999.99, ErrorMessage = "Efficiency must be between 0 and 999.99.")]
		public decimal Efficiency { get; set; }
	}
}
