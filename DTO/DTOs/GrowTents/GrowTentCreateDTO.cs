namespace DTO.DTOs.GrowTents
{
	public class GrowTentCreateDTO
	{

		[Required(ErrorMessage = "BrandId is required.")]
		public int BrandId { get; set; }

		[StringLength(100)]
		public string Dimensions { get; set; } = string.Empty;

		[StringLength(255)]
		public string Material { get; set; } = string.Empty;

		public bool Waterproof { get; set; }

		[Range(1, int.MaxValue, ErrorMessage = "Quantity must be positive.")]
		public int Quantity { get; set; }

		[Range(0.01, 99999999)]
		public decimal Price { get; set; }

		[StringLength(255)]
		public string FrameMaterial { get; set; } = string.Empty;

		public int WarrantyPeriod { get; set; }

		public string? Description { get; set; }
	}
}
