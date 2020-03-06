using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Connect.Interfaces;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Linq;
using System.Threading.Tasks;

namespace FourTwenty.IoT.Server.Components.Relays
{
    public class DoubleRelay : IoTComponent, IRelay
    {
        public DoubleRelay(int gpio1, int gpio2, GpioController gpioController) : base(new[] { gpio1, gpio2 }, gpioController)
        {
            States = new Dictionary<int, RelayState>() { { gpio1, RelayState.Closed }, { gpio2, RelayState.Closed } };
        }

        public IDictionary<int, RelayState> States { get; }

        public override void SetValue(PinValue value, int pin)
        {
            if (!Pins.Contains(pin))
                return;

            base.SetValue(value, pin);
            States[pin] = value == PinValue.Low ? RelayState.Opened : RelayState.Closed;
        }

        protected override void Initialize()
        {
            base.Initialize();
            foreach (var pin in Pins)
                Gpio.SetPinMode(pin, PinMode.Output);
        }
    }
}
