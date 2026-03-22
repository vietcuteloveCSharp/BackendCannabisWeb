namespace DTO.DTOs.Seeds
{
	public class SeedUpdateDTO
	{
		public int BreederId { get; set; }
		public int ClassifyId { get; set; }
		public string THCContent { get; set; } = string.Empty;
		public string CBDContent { get; set; } = string.Empty;
		public EStrainType StrainType { get; set; }
		public int FloweringTimeDays { get; set; }
		public decimal Yield { get; set; }
		public EDifficulty Difficulty { get; set; }
		public decimal Price { get; set; }
		public int TotalQuantity { get; set; }
		public decimal IndicaPercentage { get; set; }
		public decimal SativaPercentage { get; set; }
	}
}
