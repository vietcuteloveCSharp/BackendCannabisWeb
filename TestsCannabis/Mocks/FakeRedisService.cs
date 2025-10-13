

using System.Collections.Concurrent;

namespace TestsCannabis.Mocks
{
	public class FakeRedisService : IRedisService
	{
		private readonly ConcurrentDictionary<string, string> _store = new();
		public Task<string?> GetRedisAsync(string key)
		{
			_store.TryGetValue(key, out var value);
			return Task.FromResult(value);
		}

		public  Task RemoveRedisAsync(string key)
		{
			_store.TryRemove(key, out _);
			return Task.CompletedTask;
		}

		public Task SetRedisAsync(string key, string value, TimeSpan? expiry = null)
		{
			_store[key] = value;              
			return Task.CompletedTask;
		}
	}
}
