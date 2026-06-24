namespace Shared.DTOs.Options
{
	public class RedisSetings
	{
		public string Host { get; set; } = null!;
		public int Port { get; set; } = 6379;
		public string? Password { get; set; } // nếu có password
	}
}
