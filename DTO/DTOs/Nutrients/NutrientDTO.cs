namespace DTO.DTOs.Nutrients
{
	public class NutrientDTO
	{
		public int Id { get; set; }
		public int BrandId { get; set; }
		public string? BrandName { get; set; }
		public int NutrientTypeId { get; set; }
		public string? NutrientTypeName { get; set; }

		public int Quantity { get; set; }
		public decimal Price { get; set; }
		public int VolumeMl { get; set; }
		public string Ingredients { get; set; } = string.Empty;
		public string NpkRatio { get; set; } = string.Empty;
		public bool IsOrganic { get; set; } = false;
		public string? Description { get; set; }
		public DateTime? ExpirationDate { get; set; }
		public string StorageInstructions { get; set; } = string.Empty;
		public DateTime CreateAt { get; set; }
		public DateTime UpdateAt { get; set; }
	}
}
