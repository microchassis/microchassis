using System;

namespace MicroChassis.Tests.Core;

[TestClass]
public class MicroChassisBuilderTests
{
    [TestMethod]
    public void MicroChassisBuilder_ShouldThrowArgumentNullException_WhenHostIsNull()
    {
        var host = default(TestableMicroChassisHost);
        var chassis = new MicroChassis<TestableMicroChassisHost>();

        Assert.ThrowsException<ArgumentNullException>(() => new MicroChassisBuilder<TestableMicroChassisHost>(host!, chassis));
    }

    [TestMethod]
    public void MicroChassisBuilder_ShouldThrowArgumentNullException_WhenChassisIsNull()
    {
        var host = new TestableMicroChassisHost();
        var chassis = default(MicroChassis<TestableMicroChassisHost>);

        Assert.ThrowsException<ArgumentNullException>(() => new MicroChassisBuilder<TestableMicroChassisHost>(host, chassis!));
    }

    [TestMethod]
    public void MicroChassisBuilder_ShouldSetupChassis_WhenChassisInstanceIsPresent()
    {
        var host = new TestableMicroChassisHost();
        var mockedChassis = new Mock<IMicroChassis<TestableMicroChassisHost>>(MockBehavior.Strict);

        mockedChassis.Setup(p => p.Setup(
                It.Is<TestableMicroChassisHost>(x => ReferenceEquals(x, host))
            )
        );

        var builder = new MicroChassisBuilder<TestableMicroChassisHost>(host, mockedChassis.Object);

        mockedChassis.Verify(p => p.Setup(
                It.Is<TestableMicroChassisHost>(x => ReferenceEquals(x, host))
            ), Times.Once
        );
    }

    [TestMethod]
    public void Mount_ShouldThrowArgumentNullException_WhenModuleIsNull()
    {
        var host = new TestableMicroChassisHost();
        var chassis = new MicroChassis<TestableMicroChassisHost>();
        var builder = new MicroChassisBuilder<TestableMicroChassisHost>(host, chassis);
        var module = default(IMicroModule<TestableMicroChassisHost>);

        Assert.ThrowsException<ArgumentNullException>(() => builder.Mount(module!));
    }

    [TestMethod]
    public void Mount_ShouldSetupModule_WhenModuleInstanceIsPresent()
    {
        var host = new TestableMicroChassisHost();
        var chassis = new MicroChassis<TestableMicroChassisHost>();
        var builder = new MicroChassisBuilder<TestableMicroChassisHost>(host, chassis);
        var mockedModule = new Mock<IMicroModule<TestableMicroChassisHost>>(MockBehavior.Strict);

        mockedModule.Setup(m => m.Setup(
                It.Is<TestableMicroChassisHost>(x => ReferenceEquals(x, host))
            )
        );

        builder.Mount(mockedModule.Object);

        mockedModule.Verify(m => m.Setup(
                It.Is<TestableMicroChassisHost>(x => ReferenceEquals(x, host))
            ), Times.Once
        );
    }

    [TestMethod]
    public void Mount_ShouldSetupModule_WhenModuleTypeIsPresent()
    {
        var host = new TestableMicroChassisHost();
        var chassis = new MicroChassis<TestableMicroChassisHost>();
        var builder = new TestableMicroChassisBuilderWithModuleTypeStaticInstance(host, chassis);

        builder.MockedModuleStaticInstance.Setup(p => p.Setup(
            It.Is<TestableMicroChassisHost>(x => ReferenceEquals(x, host))
            )
        );

        builder.Mount<TestableMicroChassisBuilderWithModuleTypeStaticInstance.TestableMicroChassisModuleStaticInstance>();

        builder.MockedModuleStaticInstance.Verify(p => p.Setup(
            It.Is<TestableMicroChassisHost>(x => ReferenceEquals(x, host))
            ), Times.Once
        );
    }
}