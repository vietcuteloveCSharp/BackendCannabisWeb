namespace DTO.DTOs.Categories
{
	public class CategoryDTO
	{
		public int CategoryId { get; set; }
		public string CategoryName { get; set; } = string.Empty;
		public string? Description { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
	}
}
