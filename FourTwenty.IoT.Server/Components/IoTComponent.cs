using System;
using FourTwenty.IoT.Connect.Interfaces;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Linq;
using FourTwenty.IoT.Connect.Constants;

namespace FourTwenty.IoT.Server.Components
{
    public class IoTComponent : IModule
    {
        protected readonly GpioController Gpio;

        #region properties
        public int Id { get; set; }
        public IReadOnlyCollection<IRule> Rules { get; set; }
        public IReadOnlyCollection<IDisplayOption> DisplayOptions { get; set; }
        public string Name { get; set; }
        public WorkState RulesWorkState { get; set; } // => Rules.All(x => x.IsEnabled) ? WorkState.Running : Rules.All(x => !x.IsEnabled) ? WorkState.Stopped : WorkState.Mixed;
        public ModuleType Type { get; set; }

        #endregion

        protected IoTComponent(IReadOnlyCollection<int> pins, GpioController gpioController) : this(null, pins, gpioController)
        {
        }

        protected IoTComponent(IReadOnlyCollection<IRule> rules, IReadOnlyCollection<int> pins,
            GpioController gpioController)
        {

            Gpio = gpioController;
            Pins = pins;
            Rules = rules;
            Initialize();
        }

        public IReadOnlyCollection<int> Pins { get; }

        protected virtual void Initialize()
        {
            if (Pins == null || !Pins.Any() || Gpio == null)
                return;

            foreach (var pin in Pins)
            {
                if (!Gpio.IsPinOpen(pin))
                    Gpio.OpenPin(pin);
            }

        }

        public virtual void SetValue(PinValue value, int pin)
        {
            if (!Gpio.IsPinOpen(pin))
                Gpio.OpenPin(pin, PinMode.Output);
            Gpio.Write(pin, value);
        }
        public virtual PinValue ReadValue(int pin)
        {
            if (!Gpio.IsPinOpen(pin))
                Gpio.OpenPin(pin, PinMode.Input);
            return Gpio.Read(pin);
        }
    }
}
