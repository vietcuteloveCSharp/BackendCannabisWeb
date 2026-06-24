namespace Service.Services.ServicesRedis
{
	public class RedisService : IRedisService
	{
		private readonly IDatabase _db;
		public RedisService(IOptions<RedisSetings> redisOptions)
		{
			var setings = redisOptions.Value;
			var config = setings.Password is null
				?$"{setings.Host}:{setings.Port}":$"{setings.Host}:{setings.Port},password={setings.Password}";

			var redis = ConnectionMultiplexer.Connect(config);
			_db =redis.GetDatabase();
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
