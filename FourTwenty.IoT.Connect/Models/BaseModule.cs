using System.Collections.Generic;
using FourTwenty.IoT.Connect.Interfaces;

namespace FourTwenty.IoT.Connect.Models
{
    public abstract class BaseModule : IModule
    {
        public string Name { get; set; }
        public IReadOnlyCollection<int> Pins { get; set; }
        public IReadOnlyCollection<IRule> Rules { get; set; }

        protected BaseModule() { }

        protected BaseModule(string name) : this(name, null, null)
        {

        }

        protected BaseModule(int gpioPin, string name, IReadOnlyCollection<IRule> rules) : this(name, new[] { gpioPin }, rules)
        {
        }

        protected BaseModule(string name, IReadOnlyCollection<int> pins, IReadOnlyCollection<IRule> rules)
        {
            Name = name;
            Pins = pins;
            Rules = rules;
        }
    }
}
