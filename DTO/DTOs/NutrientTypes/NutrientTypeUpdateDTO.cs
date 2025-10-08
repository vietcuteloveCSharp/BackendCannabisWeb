namespace DTO.DTOs.NutrientTypes
{
	public class NutrientTypeUpdateDTO
	{
		[Required(ErrorMessage = "Nutrient name is required.")]
		[StringLength(150, ErrorMessage = "Nutrient name must not exceed 150 characters.")]
		public string NutrientName { get; set; } = string.Empty;

		public string? Description { get; set; }
	}
}
