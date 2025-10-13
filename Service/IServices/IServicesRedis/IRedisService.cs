
namespace Service.IServices.IServicesRedis
{
	public interface IRedisService
	{
		Task SetRedisAsync(string key, string value, TimeSpan? expiry = null);
		Task<string?> GetRedisAsync(string key);
		Task RemoveRedisAsync(string key);
	}
}
