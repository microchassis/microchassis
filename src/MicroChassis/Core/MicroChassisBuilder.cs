using System;

namespace MicroChassis
{
    public class MicroChassisBuilder<THost>
        where THost : class
    {
        private THost Host { get; }
        private IMicroChassis<THost> Chassis { get; }

        public MicroChassisBuilder(THost host, IMicroChassis<THost> chassis)
        {
            Host = host ?? throw new ArgumentNullException(nameof(host));
            Chassis = chassis ?? throw new ArgumentNullException(nameof(chassis));

            Chassis.Setup(Host);
        }

        public MicroChassisBuilder<THost> Mount(IMicroModule<THost> module)
        {
            if (module is null)
            {
                throw new ArgumentNullException(nameof(module));
            }

            module.Setup(Host);
            return this;
        }

        public MicroChassisBuilder<THost> Mount<TMicroChassisModule>()
            where TMicroChassisModule : IMicroModule<THost>, new()
        {
            return Mount(new TMicroChassisModule());
        }
    }
}