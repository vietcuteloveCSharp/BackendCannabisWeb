namespace DTO.DTOs.Dehumidifiers
{
	public class DehumidifierCreateDTO
	{
		

		[Required(ErrorMessage = "Dehumidification capacity is required.")]
		[Range(0.01, 999.99, ErrorMessage = "Dehumidification capacity must be between 0.01 and 999.99.")]
		public decimal DehumidificationCapacity { get; set; }

		[Required(ErrorMessage = "Quantity is required.")]
		[Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
		public int Quantity { get; set; }

		[Required(ErrorMessage = "BrandId is required.")]
		public int BrandId { get; set; }

		[Required(ErrorMessage = "Coverage area is required.")]
		[Range(0, 9999999.99, ErrorMessage = "Coverage area must be between 0 and 9,999,999.99.")]
		public decimal CoverageArea { get; set; }

		[Required(ErrorMessage = "Noise level is required.")]
		[Range(0, 999.99, ErrorMessage = "Noise level must be between 0 and 999.99.")]
		public decimal NoiseLevel { get; set; }

		[Required(ErrorMessage = "Power consumption is required.")]
		[Range(0, 9999999.99, ErrorMessage = "Power consumption must be between 0 and 9,999,999.99.")]
		public decimal PowerConsumption { get; set; }
	}
}
