namespace DTO.DTOs.Dehumidifiers
{
	public class DehumidifierDTO
	{
		public int DehumidifierId { get; set; }
		public decimal DehumidificationCapacity { get; set; }
		public int Quantity { get; set; }
		public int BrandId { get; set; }
		public decimal CoverageArea { get; set; }
		public decimal NoiseLevel { get; set; }
		public decimal PowerConsumption { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
	}
}
