using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace Company.MicroModules.Redis;

class RedisClientFactory : IRedisClientFactory
{
    private IRedisClientHandler ClientHandler { get; }
    private RedisClientFactoryOptions Options { get; }

    public RedisClientFactory(IOptions<RedisClientFactoryOptions> options, IRedisClientHandler clientHandler)
    {
        ClientHandler = clientHandler ?? throw new ArgumentNullException(nameof(clientHandler));
        Options = options?.Value ?? throw new ArgumentNullException(nameof(options));

        Options.Clients = new Dictionary<string, RedisClientOptions>(Options.Clients, StringComparer.OrdinalIgnoreCase);
    }

    public IConnectionMultiplexer CreateClient(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        if (Options.Clients.TryGetValue(name, out var clientOptions))
        {
            return ClientHandler.GetInstance(name, clientOptions);
        }

        throw new RedisClientFactoryException($"No options for client '{name}' found.");
    }
}