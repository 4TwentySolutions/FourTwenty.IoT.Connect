using System.Collections.Generic;
using FourTwenty.IoT.Connect.Constants;

namespace GrowIoT.Modules
{
    public class WaterPumpModule : BaseModule
    {
        public WaterPumpModule(int? gpioPin = null, List<ModuleRule> rules = null, string name = null) : base(gpioPin, rules, name)
        {
            Type = ModuleType.WaterPump;
        }
    }
}
