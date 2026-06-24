namespace Shared.Common.Response
{
	public class ApiResult
	{
		public bool Success { get; set; }
		public string Message { get; set; } = string.Empty;
		public IEnumerable<string> Errors { get; set; } = Enumerable.Empty<string>();

		public  static ApiResult Ok(string message = "")
			=> new() { Success = true, Message = message };

		public static ApiResult Fail(string message, params string[] errors)
			=> new() { Success = false, Message = message, Errors = errors };
	}
}
