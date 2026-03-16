namespace DTO.DTOs.Classifications
{
	public class ClassificationCreateDTO
	{
		public string ClassificationName { get; set; } = string.Empty;
		public int Quantity { get; set; }
		public string? Description { get; set; }
		public bool Is_Active { get; set; }
	}
}
