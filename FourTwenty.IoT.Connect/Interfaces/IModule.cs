using System.Collections.Generic;
using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Connect.Dto;

namespace FourTwenty.IoT.Connect.Interfaces
{
    public interface IModule : IPinComponent
    {
        ModuleType Type { get; }
        List<ModuleRuleDto> Rules { get; set; }
    }
}
