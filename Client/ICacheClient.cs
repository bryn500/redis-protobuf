using Redis.Models;

namespace Redis.Client;

public interface ICacheClient
{
    Task<Data[]> GetData();
}
