using System;
using Microsoft.AspNetCore.Builder;

namespace MicroChassis.AspNetCore.Tests.Extensions;

[TestClass]
public class WebApplicationExtensionsTests
{
    [TestMethod]
    public void UseMicroChassis_ShouldThrowArgumentNullException_WhenWebApplicationIsNull()
    {
        var webApplication = default(WebApplication);

        Assert.ThrowsException<ArgumentNullException>(() => webApplication!.UseMicroChassis<WebApplicationMicroChassis>());
    }

    [TestMethod]
    public void UseMicroChassis_ShouldReturnMicroChassisBuilderOfTypeWebApplication()
    {
        var webApplication = WebApplication.CreateBuilder().Build();

        var builder = webApplication.UseMicroChassis<WebApplicationMicroChassis>();

        Assert.IsInstanceOfType(builder, typeof(MicroChassisBuilder<WebApplication>));
    }
}