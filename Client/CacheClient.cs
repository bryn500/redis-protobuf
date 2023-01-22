using ProtoBuf;
using Redis.Models;
using StackExchange.Redis;

namespace Redis.Client;

public class CacheClient : ICacheClient
{
    private readonly IConnectionMultiplexer _redis;

    public CacheClient(IConnectionMultiplexer redis)
    {
        _redis = redis;
    }

    private Data[] GetUnderlyingData()
    {
        return Enumerable.Range(1, 5)
            .Select(i => new Data(i, $"{i}"))
            .ToArray();
    }

    public async Task<Data[]> GetData()
    {
        var cacheKey = "proto";
        var db = _redis.GetDatabase();

        var cacheHit = await db.StringGetAsync(cacheKey);

        if (cacheHit != RedisValue.Null)
        {
            var result = Serializer.Deserialize<Data[]>(cacheHit);
            return result;
        }
        else
        {
            var result = GetUnderlyingData();

            using var memoryStream = new MemoryStream();
            Serializer.Serialize(memoryStream, result);
            var byteArray = memoryStream.ToArray();
            await db.StringSetAsync(cacheKey, byteArray);

            return result;
        }
    }
}
