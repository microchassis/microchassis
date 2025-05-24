using System;
using System.Collections.Generic;

namespace MicroChassis
{
    public class MicroChassis<THost> : IMicroChassis<THost>
        where THost : class
    {
        private List<IMicroModule<THost>> ModulesChain { get; } = new List<IMicroModule<THost>>();

        protected void AddModule(IMicroModule<THost> module)
        {
            if (module is null)
            {
                throw new ArgumentNullException(nameof(module));
            }

            ModulesChain.Add(module);
        }

        protected void AddModule<TMicroChassisModule>()
            where TMicroChassisModule : IMicroModule<THost>, new()
        {
            AddModule(new TMicroChassisModule());
        }

        public void Setup(THost host)
        {
            if (host is null)
            {
                throw new ArgumentNullException(nameof(host));
            }

            foreach (var module in ModulesChain) module.Setup(host);
        }
    }
}