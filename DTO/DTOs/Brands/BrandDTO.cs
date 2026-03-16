namespace DTO.DTOs.Brands
{
	public class BrandDTO
	{
		public int BrandId { get; set; }
		public string BrandName { get; set; } = string.Empty;
		public string Country { get; set; } = string.Empty;
		public string? Description { get; set; }
		public string? Website { get; set; }
		public bool IsActive { get; set; } = true;
		public DateTime CreatedAt { get; set; } 
		public DateTime UpdatedAt { get; set; }
	}
}
