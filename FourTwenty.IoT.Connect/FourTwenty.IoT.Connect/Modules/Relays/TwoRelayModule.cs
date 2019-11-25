using System.Collections.Generic;
using System.Linq;
using FourTwenty.IoT.Connect.Constants;

namespace GrowIoT.Modules.Relays
{
    public class TwoRelayModule : BaseModule
    {
        public Dictionary<int, BaseModule> SubModules { get; set; } = new Dictionary<int, BaseModule>();

        public TwoRelayModule(string name, int firstGpioPin, int secondGpioPin, List<ModuleRule> rules = null) : base(rules, name)
        {
            Pins = new List<int>
            {
                firstGpioPin,
                secondGpioPin
            };

            Type = ModuleType.TwoRelay;
        }
        public TwoRelayModule AddSubModule(BaseModule subModule, int gpioPin)
        {
            SubModules.Add(gpioPin, subModule);
            return this;
        }

    }
}
