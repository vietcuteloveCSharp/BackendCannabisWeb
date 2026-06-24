namespace Shared.DTOs.DTO.Products
{
	public class ProductDTO
	{
		public int Id { get; set; }
		public string ProductName { get; set; } = string.Empty;
		public int CategoryId { get; set; }
		public string? CategoryName { get; set; }
		public int? BrandId { get; set; }
		public string? BrandName { get; set; }
		public string? ProductType { get; set; }
		public bool IsActive { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
