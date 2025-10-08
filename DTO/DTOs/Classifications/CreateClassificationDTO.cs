namespace DTO.DTOs.Classifications
{
	public class CreateClassificationDTO
	{
		public int Quantity { get; set; }
	
		public string? Description { get; set; }
		public bool Is_Active { get; set; } = true;
	}
}
