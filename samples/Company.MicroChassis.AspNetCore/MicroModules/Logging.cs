using MicroChassis;
using Microsoft.AspNetCore.Builder;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;

namespace Company.MicroChassis.AspNetCore;

public class Logging : IMicroModule<WebApplicationBuilder>, IMicroModule<WebApplication>
{
    public void Setup(WebApplicationBuilder host)
    {
        host.Services.AddSerilog(options => options
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
            .WriteTo.Console(new CompactJsonFormatter())
        );
    }

    public void Setup(WebApplication host)
    {
        host.UseSerilogRequestLogging(options =>
        {
            options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
            {
                diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value ?? string.Empty);
                diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
            };
        });
    }
}