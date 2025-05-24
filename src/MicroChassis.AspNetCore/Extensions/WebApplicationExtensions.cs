using Microsoft.AspNetCore.Builder;

namespace MicroChassis.AspNetCore;

public static class WebApplicationExtensions
{
    public static MicroChassisBuilder<WebApplication> UseMicroChassis<TMicroChassis>(this WebApplication webApplication)
        where TMicroChassis : MicroChassis<WebApplication>, new()
    {
        ArgumentNullException.ThrowIfNull(webApplication);

        return new MicroChassisBuilder<WebApplication>(webApplication, new TMicroChassis());
    }
}