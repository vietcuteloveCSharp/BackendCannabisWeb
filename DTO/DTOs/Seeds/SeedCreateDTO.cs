namespace DTO.DTOs.Seeds
{
	public class SeedCreateDTO
	{
		[Required(ErrorMessage = "Id breeder is required.")]
		public int BreederId { get; set; }

		[MaxLength(30)]
		public string THCContent { get; set; } = string.Empty;

		[MaxLength(30)]
		public string CBDContent { get; set; } = string.Empty;

		[Required]
		[EnumDataType(typeof(EStrainType))]
		public EStrainType StrainType { get; set; }

		[Required(ErrorMessage = "Id Classify is required.")]
		public int ClassifyId { get; set; }

		public int FloweringTimeDays { get; set; }

		[Range(0, 999.99)]
		public decimal Yield { get; set; }

		[Required(ErrorMessage = "Difficulty is required.")]
		[EnumDataType(typeof(EDifficulty))]
		public EDifficulty Difficulty { get; set; }

		[Range(0, 99999999.99)]
		public decimal Price { get; set; }

		[Range(0, 100)]
		public decimal IndicaPercentage { get; set; }

		[Range(0, 100)]
		public decimal SativaPercentage { get; set; }

		public int TotalQuantity { get; set; }
	}
}
