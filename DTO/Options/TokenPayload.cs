namespace DTO.Options

{
	public class TokenPayload
	{
		public string UserId { get; set; } = default!;
		public string UserName { get; set; } = default!;
		public string Role { get; set; } = default!; // "admin" | "employee" | "customer"
		public DateTime Expiration { get; set; }
	}
}
