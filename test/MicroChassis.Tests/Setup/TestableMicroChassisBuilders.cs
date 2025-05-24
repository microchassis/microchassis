namespace MicroChassis.Tests;

public class TestableMicroChassisBuilderWithModuleTypeStaticInstance : MicroChassisBuilder<TestableMicroChassisHost>
{
    internal class TestableMicroChassisModuleStaticInstance : IMicroModule<TestableMicroChassisHost>
    {
        public static Mock<IMicroModule<TestableMicroChassisHost>> MockedModuleStaticInstance { get; } = new Mock<IMicroModule<TestableMicroChassisHost>>(MockBehavior.Strict);

        public void Setup(TestableMicroChassisHost host)
        {
            MockedModuleStaticInstance.Object.Setup(host);
        }
    }

    public TestableMicroChassisBuilderWithModuleTypeStaticInstance(TestableMicroChassisHost host, MicroChassis<TestableMicroChassisHost> chassis)
        : base(host, chassis) { }

    public Mock<IMicroModule<TestableMicroChassisHost>> MockedModuleStaticInstance => TestableMicroChassisModuleStaticInstance.MockedModuleStaticInstance;
}