using Microsoft.AspNetCore.Builder;

namespace MicroChassis.AspNetCore.Tests.Core;

[TestClass]
public class WebApplicationBuilderMicroChassisTests
{
    [TestMethod]
    public void WebApplicationBuilderMicroChassis_ShouldBeMicroChassisOfWebApplicationBuilder()
    {
        Assert.IsInstanceOfType(new WebApplicationBuilderMicroChassis(), typeof(IMicroChassis<WebApplicationBuilder>));
    }
}