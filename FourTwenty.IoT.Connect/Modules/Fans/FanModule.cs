using System.Collections.Generic;
using FourTwenty.IoT.Connect.Constants;

namespace FourTwenty.IoT.Connect.Modules.Fans
{
    public class FanModule : BaseModule
    {
        public FanModule(string name, int? gpioPin = null, List<ModuleRule> rules = null) : base(gpioPin, rules, name)
        {
            Type = ModuleType.Fan;
        }
    }
}
