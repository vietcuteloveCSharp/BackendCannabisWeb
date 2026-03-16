namespace DTO.DTOs.Classifications
{
	public class ClassificationDTO
	{
		public int ClassificationId { get; set; }
		public string ClassificationName { get; set; } = string.Empty;
		public int Quantity { get; set; }
		public string? Description { get; set; }
		public bool Is_Active { get; set; }
		public DateTime CreatedAt { get; set; } 
		public DateTime UpdatedAt { get; set; }
	}
}
