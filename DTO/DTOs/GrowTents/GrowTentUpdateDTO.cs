namespace DTO.DTOs.GrowTents
{
	public class GrowTentUpdateDTO
	{

		[StringLength(100)]
		public string Dimensions { get; set; } = string.Empty;
		public int BrandId { get; set; }

		[StringLength(255)]
		public string Material { get; set; } = string.Empty;

		public bool Waterproof { get; set; }

		[Range(1, int.MaxValue)]
		public int Quantity { get; set; }

		[Range(0.01, 99999999)]
		public decimal Price { get; set; }

		[StringLength(255)]
		public string FrameMaterial { get; set; } = string.Empty;

		public int WarrantyPeriod { get; set; }

		public string? Description { get; set; }
	


	}
}
