using StackExchange.Redis;

namespace Company.MicroModules.Redis;

class RedisClientHandler : IRedisClientHandler
{
    private Dictionary<string, IConnectionMultiplexer> Instances { get; } = new Dictionary<string, IConnectionMultiplexer>(StringComparer.OrdinalIgnoreCase);
    private ReaderWriterLockSlim InstancesLock { get; } = new ReaderWriterLockSlim();

    public IConnectionMultiplexer GetInstance(string name, RedisClientOptions options)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(options);

        InstancesLock.EnterUpgradeableReadLock();
        try
        {
            var connectionMultiplexer = Instances.GetValueOrDefault(name);
            if (connectionMultiplexer != default)
            {
                return connectionMultiplexer;
            }

            InstancesLock.EnterWriteLock();
            try
            {
                connectionMultiplexer = Instances.GetValueOrDefault(name);
                if (connectionMultiplexer == default)
                {
                    connectionMultiplexer = ConnectionMultiplexer.Connect(options.Configuration);
                    Instances.Add(name, connectionMultiplexer);
                }
                return connectionMultiplexer;
            }
            finally
            {
                InstancesLock.ExitWriteLock();
            }
        }
        finally
        {
            InstancesLock.ExitUpgradeableReadLock();
        }
    }
}