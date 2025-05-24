using Microsoft.Extensions.Hosting;

namespace MicroChassis.NETCore;

public static class IHostBuilderExtensions
{
    public static MicroChassisBuilder<IHostBuilder> AddMicroChassis<TMicroChassis>(this IHostBuilder hostBuilder)
        where TMicroChassis : MicroChassis<IHostBuilder>, new()
    {
        ArgumentNullException.ThrowIfNull(hostBuilder);

        return new MicroChassisBuilder<IHostBuilder>(hostBuilder, new TMicroChassis());
    }
}