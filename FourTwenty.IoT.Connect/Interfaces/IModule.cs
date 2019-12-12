using System.Collections.Generic;
using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Connect.Dto;
using FourTwenty.IoT.Connect.Entities;

namespace FourTwenty.IoT.Connect.Interfaces
{
    public interface IModule
    {
        List<int> Pins { get; }
        ModuleType Type { get; }
        List<ModuleRuleDto> Rules { get; set; }
    }
}
