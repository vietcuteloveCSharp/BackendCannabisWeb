namespace DTO.DTOs.GrowLights
{
	public class GrowLightDTO
	{
		public int Id { get; set; }
		public string? ModelNumber { get; set; }
		public decimal Price { get; set; }
		public int Wattage { get; set; }
		public int BrandId { get; set; }
		public string? BrandName { get; set; }
		public int Quantity { get; set; }
		public int CoverageArea { get; set; }
		public int WarrantyPeriod { get; set; }
		public int Lifespan { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
	}
}
