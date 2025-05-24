using System;
using Microsoft.AspNetCore.Builder;

namespace MicroChassis.AspNetCore.Tests.Extensions;

[TestClass]
public class WebApplicationBuilderExtensionsTests
{
    [TestMethod]
    public void AddMicroChassis_ShouldThrowArgumentNullException_WhenWebApplicationBuilderIsNull()
    {
        var webApplicationBuilder = default(WebApplicationBuilder);

        Assert.ThrowsException<ArgumentNullException>(() => webApplicationBuilder!.AddMicroChassis<WebApplicationBuilderMicroChassis>());
    }

    [TestMethod]
    public void AddMicroChassis_ShouldReturnMicroChassisBuilderOfTypeWebApplicationBuilder()
    {
        var webApplicationBuilder = WebApplication.CreateBuilder();

        var builder = webApplicationBuilder.AddMicroChassis<WebApplicationBuilderMicroChassis>();

        Assert.IsInstanceOfType(builder, typeof(MicroChassisBuilder<WebApplicationBuilder>));
    }
}