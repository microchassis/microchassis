using MicroChassis;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Company.MicroModules.Redis;

public class Redis : IMicroModule<WebApplicationBuilder>
{
    private const string RedisConfigurationSectionName = "Redis";

    public void Setup(WebApplicationBuilder host)
    {
        if (!host.Configuration.GetSection(RedisConfigurationSectionName).Exists())
        {
            return;
        }

        host.Services
            .AddSingleton<IRedisClientHandler, RedisClientHandler>()
            .AddTransient<IRedisClientFactory, RedisClientFactory>()
            .AddOptions<RedisClientFactoryOptions>()
            .BindConfiguration(RedisConfigurationSectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();
    }
}