namespace DTO.DTOs.GrowLights
{
	public class GrowLightUpdateDTO 
	{
		

		[Range(0, int.MaxValue, ErrorMessage = "Quantity must be non-negative.")]
		public int Quantity { get; set; }

		[Range(1, int.MaxValue, ErrorMessage = "Wattage must be a positive number.")]
		public int Wattage { get; set; }

		[Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
		public decimal Price { get; set; }

		[Range(0, int.MaxValue, ErrorMessage = "CoverageArea must be non-negative.")]
		public int CoverageArea { get; set; }

		[Range(0, 120, ErrorMessage = "WarrantyPeriod must be between 0 and 120 months.")]
		public int WarrantyPeriod { get; set; }
					
		[Range(0, int.MaxValue, ErrorMessage = "Lifespan must be non-negative.")]
		public int Lifespan { get; set; }

		[MaxLength(100, ErrorMessage = "ModelNumber cannot exceed 100 characters.")]
		public string ModelNumber { get; set; } = string.Empty;
	}
}
