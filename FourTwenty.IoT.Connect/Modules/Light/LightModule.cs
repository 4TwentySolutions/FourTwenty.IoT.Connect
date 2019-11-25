using System.Collections.Generic;
using FourTwenty.IoT.Connect.Constants;

namespace FourTwenty.IoT.Connect.Modules.Light
{
    public class LightModule : BaseModule
    {
        public LightModule(string name, List<ModuleRule> rules = null) : base(rules, name)
        {
            Type = ModuleType.Light;
        }
    }
}
