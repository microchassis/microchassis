using Microsoft.AspNetCore.Builder;

namespace MicroChassis.AspNetCore.Tests.Core;

[TestClass]
public class WebApplicationMicroChassisTests
{
    [TestMethod]
    public void WebApplicationMicroChassis_ShouldBeMicroChassisOfTypeWebApplication()
    {
        Assert.IsInstanceOfType(new WebApplicationMicroChassis(), typeof(IMicroChassis<WebApplication>));
    }
}