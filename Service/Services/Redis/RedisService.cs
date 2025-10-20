namespace Service.Services.ServicesRedis
{
	public class RedisService : IRedisService
	{
		private readonly IDatabase _db;
		public RedisService(IConfiguration config)
		{
			var redis = ConnectionMultiplexer.Connect(config.GetConnectionString("Redis") ?? throw new InvalidOperationException("Redis connection string is not configured."));
			_db = redis.GetDatabase();
		}
		// Lấy giá trị từ Redis
		public async Task<string?> GetRedisAsync(string key)
		{
			return await _db.StringGetAsync(key);
		}
		// Xóa giá trị khỏi Redis
		public async Task RemoveRedisAsync(string key)
		{
			await _db.KeyDeleteAsync(key);
		}
		// Đặt giá trị trong Redis với thời gian hết hạn tùy chọn
		public async Task SetRedisAsync(string key, string value, TimeSpan? expiry = null)
		{
			await _db.StringSetAsync(key, value, expiry);
		}
	}
}
