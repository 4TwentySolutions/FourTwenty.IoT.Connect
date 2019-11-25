using System.Collections.Generic;
using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Connect.Modules;

namespace FourTwenty.IoT.Connect.Interfaces.Modules
{
    public interface IModule
    {
        List<int> Pins { get; }
        ModuleType Type { get; }
        List<ModuleRule> Rules { get; set; }
    }
}
