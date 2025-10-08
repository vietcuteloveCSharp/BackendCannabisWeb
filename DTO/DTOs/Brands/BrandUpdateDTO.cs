namespace DTO.DTOs.Brands
{
	public class BrandUpdateDTO
	{
		[Required(ErrorMessage = "Brand name is required.")]
		[StringLength(255, ErrorMessage = "Brand name cannot exceed 255 characters.")]
		public string BrandName { get; set; } = string.Empty;
		[StringLength(150, ErrorMessage = "Country name cannot exceed 150 characters.")]
		public string Country { get; set; } = string.Empty;
		public string? Description { get; set; }
		[StringLength(255, ErrorMessage = "Website link cannot exceed 255 characters.")]
		public string? Website { get; set; }
		public bool IsActive { get; set; } 
	
	}
}
