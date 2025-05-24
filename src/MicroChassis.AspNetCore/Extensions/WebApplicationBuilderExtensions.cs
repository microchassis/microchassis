using Microsoft.AspNetCore.Builder;

namespace MicroChassis.AspNetCore;

public static class WebApplicationBuilderExtensions
{
    public static MicroChassisBuilder<WebApplicationBuilder> AddMicroChassis<TMicroChassis>(this WebApplicationBuilder webApplicationBuilder)
        where TMicroChassis : MicroChassis<WebApplicationBuilder>, new()
    {
        ArgumentNullException.ThrowIfNull(webApplicationBuilder);

        return new MicroChassisBuilder<WebApplicationBuilder>(webApplicationBuilder, new TMicroChassis());
    }
}