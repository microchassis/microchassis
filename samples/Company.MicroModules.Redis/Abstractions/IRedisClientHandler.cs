using StackExchange.Redis;

namespace Company.MicroModules.Redis;

interface IRedisClientHandler
{
    IConnectionMultiplexer GetInstance(string name, RedisClientOptions options);
}