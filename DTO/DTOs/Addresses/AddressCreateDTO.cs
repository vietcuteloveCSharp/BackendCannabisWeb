namespace DTO.DTOs.Addresses
{
	public class AddressCreateDTO
	{
		[StringLength(150, ErrorMessage = "Country name cannot exceed 150 characters.")]
		public string Country { get; set; } = string.Empty;

		[StringLength(150, ErrorMessage = "Province name cannot exceed 150 characters.")]
		public string Province { get; set; } = string.Empty;

		[StringLength(150, ErrorMessage = "District name cannot exceed 150 characters.")]
		public string District { get; set; } = string.Empty;

		[StringLength(150, ErrorMessage = "Commune name cannot exceed 150 characters.")]
		public string Commune { get; set; } = string.Empty;

		[StringLength(150, ErrorMessage = "Road/Village/Hamlet cannot exceed 150 characters.")]
		public string Road_Village_Hamlet { get; set; } = string.Empty;

		[StringLength(20, ErrorMessage = "House number cannot exceed 20 characters.")]
		public string HouseNumber { get; set; } = string.Empty;

		[StringLength(30, ErrorMessage = "Postal code cannot exceed 30 characters.")]
		public string PostalCode { get; set; } = string.Empty;

		public bool IsDefault { get; set; } = false;
	}
}
