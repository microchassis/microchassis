using StackExchange.Redis;

namespace Company.MicroModules.Redis;

public interface IRedisClientFactory
{
    IConnectionMultiplexer CreateClient(string name);
}