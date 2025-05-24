using MicroChassis;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Trace;

namespace Company.MicroChassis.AspNetCore;

public class Tracing : IMicroModule<WebApplicationBuilder>
{
    public void Setup(WebApplicationBuilder host)
    {
        host.Services
            .AddOpenTelemetry()
            .WithTracing(tracing => tracing
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddJaegerExporter(options => options
                    .Endpoint = new Uri("https://jaeger-server-name:14268/api/traces"))
            );
    }
}