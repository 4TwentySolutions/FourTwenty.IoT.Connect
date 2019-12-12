using System;
using System.Collections.Generic;
using System.Text;
using FourTwenty.IoT.Connect.Constants;

namespace FourTwenty.IoT.Connect.Dto
{
    public class GrowBoxModuleDto
    {
        public string Name { get; set; }
        public ModuleType Type { get; set; }
        public int? Pin { get; set; }
        public string Pins { get; set; }

        public List<ModuleRuleDto> Rules { get; set; }
    }
}
