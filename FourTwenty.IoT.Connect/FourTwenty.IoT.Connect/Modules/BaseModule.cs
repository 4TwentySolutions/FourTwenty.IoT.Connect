﻿using System.Collections.Generic;
using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Connect.Interfaces.Modules;

namespace GrowIoT.Modules
{
    public abstract class BaseModule : IModule
    {
        public string Name { get; set; }
        public List<int> Pins { get; set; }
        public ModuleType Type { get; protected set; }
        public List<ModuleRule> Rules { get; set; }

        protected BaseModule(List<ModuleRule> rules = null, string name = null)
        {
            Rules = rules;
            Name = name;
        }

        protected BaseModule(int? gpioPin = null, List<ModuleRule> rules = null, string name = null) : this(rules, name)
        {
            if (gpioPin.HasValue)
                Pins = new List<int>
                {
                    gpioPin.Value

                };
        }
    }
}
