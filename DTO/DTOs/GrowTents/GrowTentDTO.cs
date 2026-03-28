namespace DTO.DTOs.GrowTents
{
	public class GrowTentDTO
	{
		public int Id { get; set; }
		public int BrandId { get; set; }
		public string Dimensions { get; set; } = string.Empty;
		public string Material { get; set; } = string.Empty;
		public bool Waterproof { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
		public string FrameMaterial { get; set; } = string.Empty;
		public int WarrantyPeriod { get; set; }
		public string? Description { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
	}
}
