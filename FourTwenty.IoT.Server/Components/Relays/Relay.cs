using System;
using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Connect.Interfaces;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Linq;
using System.Threading.Tasks;
using FourTwenty.IoT.Connect.Models;

namespace FourTwenty.IoT.Server.Components.Relays
{
    public class Relay : IoTComponent, IRelay
    {
        public Relay(IReadOnlyCollection<int> pins, GpioController gpioController) : base(pins, gpioController)
        {
            States = new Dictionary<int, RelayState>(pins.Select(x => new KeyValuePair<int, RelayState>(x, RelayState.Closed)));
        }

        public IDictionary<int, RelayState> States { get; }
        public event EventHandler<ModuleResponseEventArgs> StateChanged;

        public override void SetValue(PinValue value, int pin)
        {
            if (!Pins.Contains(pin))
                return;

            base.SetValue(value, pin);
            States[pin] = value == PinValue.Low ? RelayState.Opened : RelayState.Closed;
            StateChanged?.Invoke(this, new ModuleResponseEventArgs(new ModuleResponse(true, new RelayData(pin, States[pin]))));
        }

        protected override void Initialize()
        {
            base.Initialize();
            foreach (var pin in Pins)
                Gpio.SetPinMode(pin, PinMode.Output);
        }
    }
}
