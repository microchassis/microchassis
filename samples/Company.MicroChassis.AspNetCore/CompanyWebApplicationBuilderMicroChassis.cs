using MicroChassis.AspNetCore;

namespace Company.MicroChassis.AspNetCore;

public class CompanyWebApplicationBuilderMicroChassis : WebApplicationBuilderMicroChassis
{
    public CompanyWebApplicationBuilderMicroChassis()
    {
        AddModule<Hosting>();
        AddModule<Logging>();
        AddModule<Tracing>();
        AddModule<Metrics>();
    }
}