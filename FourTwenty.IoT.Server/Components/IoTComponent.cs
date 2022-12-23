using System;
using FourTwenty.IoT.Connect.Interfaces;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Linq;
using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Connect.Interfaces.Rules;
using FourTwenty.IoT.Connect.Models;
using FourTwenty.IoT.Connect.Rules;
using FourTwenty.IoT.Server.Interfaces;
using GrowIoT.Rules;
using Microsoft.Extensions.Logging;

namespace FourTwenty.IoT.Server.Components
{
    public class IoTComponent : IComponent
    {
        protected readonly GpioController Gpio;
        public IIoTRuntimeService IoTRuntimeService { get; set; }

        protected ILogger _logger;

        #region properties
        public int Id { get; set; }
        public IReadOnlyCollection<CronRule> Rules { get; set; }
        public IReadOnlyCollection<DisplayRule> DisplayOptions { get; set; }
        public IReadOnlyCollection<IAction> Actions { get; set; }

        public string Name { get; set; }
        public WorkState RulesWorkState { get; set; } // => Rules.All(x => x.IsEnabled) ? WorkState.Running : Rules.All(x => !x.IsEnabled) ? WorkState.Stopped : WorkState.Mixed;
        public ComponentType ComponentType { get; set; }

        #endregion

        public IoTComponent(IReadOnlyCollection<PinNameItem> pins, GpioController gpioController, ILogger logger = null) : this(null, pins, gpioController, logger) { }

        protected IoTComponent(IReadOnlyCollection<CronRule> rules, IReadOnlyCollection<PinNameItem> pins, GpioController gpioController, ILogger logger = null)
        {

            Gpio = gpioController;
            Pins = pins.Select(x => x.Pin).ToList();
            Rules = rules;
            PinsNames = pins;
            _logger = logger;
        }

        public IReadOnlyCollection<PinNameItem> PinsNames { get; }
        public IReadOnlyCollection<int> Pins { get; }

        public virtual void Initialize()
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

        public void UpdateDisplayOptions(IEnumerable<DisplayRule> options)
        {
            DisplayOptions = new List<DisplayRule>(options);
        }

        public void SetLogger(ILogger logger)
        {
            _logger = logger;
        }
    }
}
