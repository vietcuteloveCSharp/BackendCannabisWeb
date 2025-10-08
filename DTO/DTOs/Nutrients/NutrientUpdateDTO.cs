namespace DTO.DTOs.Nutrients
{
	public class NutrientUpdateDTO
	{
		public int Quantity { get; set; }

		[Range(0, double.MaxValue)]
		public decimal Price { get; set; }

		public int VolumeMl { get; set; }

		[StringLength(255)]
		public string Ingredients { get; set; } = string.Empty;

		[StringLength(50)]
		public string NpkRatio { get; set; } = string.Empty;

		public bool IsOrganic { get; set; } = false;

		public string? Description { get; set; }

		public DateTime? ExpirationDate { get; set; }

		[StringLength(255)]
		public string StorageInstructions { get; set; } = string.Empty;
	}
}
