using System.Collections.Generic;
using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Connect.Dto;
using FourTwenty.IoT.Connect.Interfaces;

namespace FourTwenty.IoT.Connect.Models
{
    public abstract class BaseModule : IModule
    {
        public string Name { get; set; }
        public List<int> Pins { get; set; }
        public ModuleType Type { get; protected set; }
        public List<ModuleRuleDto> Rules { get; set; }

        protected BaseModule() { }

        protected BaseModule(List<ModuleRuleDto> rules = null, string name = null)
        {
            Rules = rules;
            Name = name;
        }

        protected BaseModule(int? gpioPin = null, List<ModuleRuleDto> rules = null, string name = null) : this(rules, name)
        {
            if (gpioPin.HasValue)
                Pins = new List<int>
                {
                    gpioPin.Value

                };
        }
    }
}
