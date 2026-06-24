namespace Shared.DTOs.DTO.User.Users
{
	public class UserFilterDTO
	{
		public string? SearchTerm { get; set; } // Tìm theo Name, Username, Email
		public int? RoleId { get; set; }       // Lọc theo Role

		// Pagination params
		public int PageNumber { get; set; } = 1;
		public int PageSize { get; set; } = 10;
	}
}
