using MicroChassis;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Metrics;
using Prometheus;

namespace Company.MicroChassis.AspNetCore;

public class Metrics : IMicroModule<WebApplicationBuilder>
{
    public void Setup(WebApplicationBuilder host)
    {
        host.Services
            .AddOpenTelemetry()
            .WithMetrics(metrics => metrics
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddMeter("Microsoft.AspNetCore.Hosting")
                .AddMeter("Microsoft.AspNetCore.Server.Kestrel")
                .AddMeter("System.Net.Http")
                .AddMeter("System.Net.NameResolution")
                .AddPrometheusExporter()
            );

        host.Services
            .AddMetricServer(options =>
            {
                options.Url = "/metrics";
                options.Port = 8081;
            });
    }
}