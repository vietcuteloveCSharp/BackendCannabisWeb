using System.Text.Json.Serialization;

namespace TestsCannabis
{
	public class ErrorResponse
	{
		public bool Success { get; set; }
		public int Status { get; set; }
		public string? Error { get; set; }
		public string? Detail { get; set; }
	}
}

