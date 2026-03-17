namespace DTO.DTOs.Seeds
{
	public class SeedCreateRequestDTO
	{
		// --- Thông tin chung (Bảng Product) ---
		public string ProductName { get; set; } = string.Empty;
		public int CategoryId { get; set; }
		public int? BrandId { get; set; }
		public string? Description { get; set; }

		// --- Thông số kỹ thuật (Bảng Seed) ---
		public int BreederId { get; set; }
		public int ClassifyId { get; set; }
		public string THCContent { get; set; } = string.Empty;
		public string CBDContent { get; set; } = string.Empty;
		public EStrainType StrainType { get; set; }
		public int FloweringTimeDays { get; set; }
		public decimal Yield { get; set; }
		public decimal Price { get; set; }
		public int TotalQuantity { get; set; }
		public decimal IndicaPercentage { get; set; }
		public decimal SativaPercentage { get; set; }
	}
}
