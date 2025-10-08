namespace DTO.Response
{
	public class TokenResultDTO
	{
		public string AccessToken { get; set; } = default!;
		public string RefreshToken { get; set; } = default!;
		public UserSummaryDTO User { get; set; } = default!;
	}
}
