using Microsoft.Extensions.Hosting;

namespace MicroChassis.NETCore.Tests.Core;

[TestClass]
public class HostBuilderMicroChassisTests
{
    [TestMethod]
    public void HostBuilderMicroChassis_Should_BeAMicroChassis()
    {
        Assert.IsInstanceOfType(new HostBuilderMicroChassis(), typeof(IMicroChassis<IHostBuilder>));
    }
}