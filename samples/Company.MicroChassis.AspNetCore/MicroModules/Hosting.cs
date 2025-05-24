using MicroChassis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace Company.MicroChassis.AspNetCore;

public class Hosting : IMicroModule<WebApplicationBuilder>
{
    public void Setup(WebApplicationBuilder host)
    {
        host.WebHost.UseKestrel(options => options.ListenLocalhost(8080));
    }
}