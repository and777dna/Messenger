using System.Text.Json;
using StackExchange.Redis;

namespace Messenger.Caching;

public class RedisCacheService(IConnectionMultiplexer redis) : ICacheService
{
    private IDatabase Database => redis.GetDatabase();

    public async Task<T?> GetAsync<T>(string key)
    {
        var value = await Database.StringGetAsync(key);
        return value.IsNullOrEmpty ? default : JsonSerializer.Deserialize<T>(value!);
    }

    public Task SetAsync<T>(string key, T value, TimeSpan? expiry = null) =>
        Database.StringSetAsync(key, JsonSerializer.Serialize(value), expiry);

    public Task RemoveAsync(string key) =>
        Database.KeyDeleteAsync(key);
}
