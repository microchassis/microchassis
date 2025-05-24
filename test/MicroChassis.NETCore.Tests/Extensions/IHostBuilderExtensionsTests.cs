using System;
using Microsoft.Extensions.Hosting;

namespace MicroChassis.NETCore.Tests.Extensions;

[TestClass]
public class IHostBuilderExtensionsTests
{
    [TestMethod]
    public void AddMicroChassis_ShouldThrowArgumentNullException_WhenHostBuilderIsNull()
    {
        var hostBuilder = default(IHostBuilder);

        Assert.ThrowsException<ArgumentNullException>(() => hostBuilder!.AddMicroChassis<HostBuilderMicroChassis>());
    }

    [TestMethod]
    public void AddMicroChassis_ShouldReturnMicroChassisBuilderOfTypeIHostBuilder()
    {
        var mockedHostBuilder = new Mock<IHostBuilder>(MockBehavior.Strict); ;

        var builder = mockedHostBuilder.Object.AddMicroChassis<HostBuilderMicroChassis>();

        Assert.IsInstanceOfType(builder, typeof(MicroChassisBuilder<IHostBuilder>));
    }
}