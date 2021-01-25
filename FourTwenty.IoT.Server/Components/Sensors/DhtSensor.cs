using System;
using FourTwenty.IoT.Connect.Interfaces;
using Iot.Device.DHTxx;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Linq;
using System.Threading.Tasks;
using FourTwenty.IoT.Connect.Models;
using FourTwenty.IoT.Server.Models;

namespace FourTwenty.IoT.Server.Components.Sensors
{
    public class DhtSensor : IoTComponent, ISensor
    {
        private readonly DhtBase _sensor;

        public DhtSensor(int gpioPin, GpioController controller, IReadOnlyCollection<IRule> rules) : base(rules, new[] { gpioPin }, controller)
        {
            _sensor = new Dht11(gpioPin, PinNumberingScheme.Logical, controller);
        }
        public DhtSensor(int gpioPin, GpioController controller) : base(new[] { gpioPin }, controller)
        {
            _sensor = new Dht11(gpioPin, PinNumberingScheme.Logical, controller, true);
        }

        public int ActivePin => Pins.FirstOrDefault();

        public event EventHandler<ModuleResponseEventArgs> DataReceived;

        protected override void Initialize()
        {
            var pin = Pins?.FirstOrDefault();
            if (pin > 0)
            {
                if (!Gpio.IsPinOpen(pin.Value))
                    Gpio.OpenPin(pin.GetValueOrDefault(), PinMode.Input);
            }
        }

        public ValueTask<object> GetData()
        {
            var tmp = _sensor.Temperature.DegreesCelsius;
            var hum = _sensor.Humidity.Value;

            var data = new DhtData(_sensor.IsLastReadSuccessful)
            {
                Humidity = hum,
                Temperature = tmp
            };

            DataReceived?.Invoke(this, new ModuleResponseEventArgs(data));

            return new ValueTask<object>(data);
        }
    }
}
