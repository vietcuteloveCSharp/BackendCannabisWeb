namespace DTO.DTOs.NutrientTypes
{
	public class NutrientTypeDTO
	{
		public int NutrientTypeId { get; set; }
		public string NutrientName { get; set; } = string.Empty;
		public string? Description { get; set; }
		public DateTime CreateAt { get; set; }
		public DateTime UpdateAt { get; set; }
	}
}
