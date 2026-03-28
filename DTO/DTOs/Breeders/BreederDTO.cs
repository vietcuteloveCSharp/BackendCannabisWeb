namespace DTO.DTOs.Breeders
{
	public class BreederDTO
	{
		public int Id { get; set; }
		public string BreederName { get; set; } = string.Empty;
		public string Country { get; set; } = string.Empty;
		public string? Description { get; set; }
		public string? Website { get; set; }
		public bool IsActive { get; set; }
		public string Email { get; set; } = string.Empty;
		public string PhoneNumber { get; set; } = string.Empty;
		public DateTime CreatedAt { get; set; }
		public DateTime UpdateAt { get; set; }
	}

}

