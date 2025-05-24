using System;
using System.Collections.Generic;
using System.Linq;

namespace MicroChassis.Tests;

public class TestableMicroChassisWithModuleNullInstance : MicroChassis<TestableMicroChassisHost>
{
    public TestableMicroChassisWithModuleNullInstance() => AddModule(default);
}

public class TestableMicroChassisWithModuleInstance : MicroChassis<TestableMicroChassisHost>
{
    public TestableMicroChassisWithModuleInstance()
    {
        MockedModuleInstance = new Mock<IMicroModule<TestableMicroChassisHost>>(MockBehavior.Strict);
        AddModule(MockedModuleInstance.Object);
    }

    public Mock<IMicroModule<TestableMicroChassisHost>> MockedModuleInstance { get; }
}

public class TestableMicroChassisWithModuleTypeStaticInstance : MicroChassis<TestableMicroChassisHost>
{
    private class TestableMicroChassisModuleStaticInstance : IMicroModule<TestableMicroChassisHost>
    {
        public static Mock<IMicroModule<TestableMicroChassisHost>> MockedModuleStaticInstance { get; } = new Mock<IMicroModule<TestableMicroChassisHost>>(MockBehavior.Strict);

        public void Setup(TestableMicroChassisHost host)
        {
            MockedModuleStaticInstance.Object.Setup(host);
        }
    }

    public TestableMicroChassisWithModuleTypeStaticInstance()
    {
        AddModule<TestableMicroChassisModuleStaticInstance>();
    }

    public Mock<IMicroModule<TestableMicroChassisHost>> MockedModuleStaticInstance => TestableMicroChassisModuleStaticInstance.MockedModuleStaticInstance;
}

public class TestableMicroChassisWithModulesInstances : MicroChassis<TestableMicroChassisHost>
{
    public TestableMicroChassisWithModulesInstances(int count)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);

        MockedModulesInstances = Enumerable
                                        .Range(1, count)
                                        .Select(i => new Mock<IMicroModule<TestableMicroChassisHost>>(MockBehavior.Strict))
                                        .Select(m => { AddModule(m.Object); return m; })
                                        .ToList();
    }

    public List<Mock<IMicroModule<TestableMicroChassisHost>>> MockedModulesInstances { get; }
}