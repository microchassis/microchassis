using MicroChassis.AspNetCore;

namespace Company.MicroChassis.AspNetCore;

public class CompanyWebApplicationMicroChassis : WebApplicationMicroChassis
{
    public CompanyWebApplicationMicroChassis()
    {
        AddModule<Logging>();
    }
}