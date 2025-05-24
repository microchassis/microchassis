using System;
using System.IO;

namespace MicroChassis.Tests.Core;

[TestClass]
public class MicroChassisTests
{
    [TestMethod]
    public void MicroChassis_ShouldThrowArgumentNullException_WhenModuleIsNull()
    {
        Assert.ThrowsException<ArgumentNullException>(() => new TestableMicroChassisWithModuleNullInstance());
    }

    [TestMethod]
    public void Setup_ShouldThrowArgumentNullException_WhenHostIsNull()
    {
        var host = (TestableMicroChassisHost)default;
        var chassis = new MicroChassis<TestableMicroChassisHost>();

        Assert.ThrowsException<ArgumentNullException>(() => chassis.Setup(host));
    }

    [TestMethod]
    public void Setup_ShouldSetupModule_WhenModuleInstanceIsPresent()
    {
        var host = new TestableMicroChassisHost();
        var chassis = new TestableMicroChassisWithModuleInstance();

        chassis.MockedModuleInstance.Setup(p => p.Setup(
            It.Is<TestableMicroChassisHost>(x => ReferenceEquals(x, host))
            )
        );

        chassis.Setup(host);

        chassis.MockedModuleInstance.Verify(p => p.Setup(
            It.Is<TestableMicroChassisHost>(x => ReferenceEquals(x, host))
            ), Times.Once
        );
    }

    [TestMethod]
    public void Setup_ShouldSetupModule_WhenModuleTypeIsPresent()
    {
        var host = new TestableMicroChassisHost();
        var chassis = new TestableMicroChassisWithModuleTypeStaticInstance();

        chassis.MockedModuleStaticInstance.Setup(p => p.Setup(
            It.Is<TestableMicroChassisHost>(x => ReferenceEquals(x, host))
            )
        );

        chassis.Setup(host);

        chassis.MockedModuleStaticInstance.Verify(p => p.Setup(
            It.Is<TestableMicroChassisHost>(x => ReferenceEquals(x, host))
            ), Times.Once
        );
    }

    [TestMethod]
    public void Setup_ShouldSetupModulesInOrder_WhenMultipleModulesArePresent()
    {
        var host = new TestableMicroChassisHost();
        var chassis = new TestableMicroChassisWithModulesInstances(10);

        var index = 0;
        var order = 0;

        chassis.MockedModulesInstances.ForEach(mockedModule =>
        {
            var expectedOrder = index++;
            mockedModule
                .Setup(p => p.Setup(
                    It.Is<TestableMicroChassisHost>(x => ReferenceEquals(x, host))
                ))
                .Callback<TestableMicroChassisHost>(x =>
                {
                    Assert.AreEqual(expectedOrder, order++, "Modules were set up out of order.");
                });
        });

        chassis.Setup(host);

        chassis.MockedModulesInstances.ForEach(mockedModule =>
        {
            mockedModule.Verify(p => p.Setup(
                    It.Is<TestableMicroChassisHost>(x => ReferenceEquals(x, host))
                ), Times.Once
            );
        });
    }

    [TestMethod]
    public void Setup_ShouldStopModulesSetup_WhenModuleThrowsException()
    {
        var host = new TestableMicroChassisHost();
        var chassis = new TestableMicroChassisWithModulesInstances(8);
        var index = 0;
        var failingIndex = 2;

        chassis.MockedModulesInstances.ForEach(mockedModule =>
        {
            if (index == failingIndex)
            {
                mockedModule.Setup(p => p.Setup(It.Is<TestableMicroChassisHost>(x => ReferenceEquals(x, host))))
                            .Throws<IOException>();
            }
            else
            {
                mockedModule.Setup(p => p.Setup(It.Is<TestableMicroChassisHost>(x => ReferenceEquals(x, host))));
            }
            index++;
        });

        Assert.ThrowsException<IOException>(() => chassis.Setup(host));

        index = 0;
        chassis.MockedModulesInstances.ForEach(mockedModule =>
        {
            if (index <= failingIndex)
            {
                mockedModule.Verify(p => p.Setup(
                        It.Is<TestableMicroChassisHost>(x => ReferenceEquals(x, host))
                    ), Times.Once
                );
            }
            else
            {
                mockedModule.Verify(p => p.Setup(
                        It.Is<TestableMicroChassisHost>(x => ReferenceEquals(x, host))
                    ), Times.Never
                );
            }
            index++;
        });
    }
}