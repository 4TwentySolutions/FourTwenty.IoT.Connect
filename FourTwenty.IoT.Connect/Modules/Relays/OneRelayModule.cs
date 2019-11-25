using System.Collections.Generic;
using FourTwenty.IoT.Connect.Constants;

namespace FourTwenty.IoT.Connect.Modules.Relays
{
    public class OneRelayModule : BaseModule
    {
        public OneRelayModule(int gpioPin, List<ModuleRule> rules) : base(gpioPin, rules)
        {
            Type = ModuleType.Relay;
        }
    }
}
