using System;
using FourTwenty.IoT.Connect.Interfaces;
using Iot.Device.DHTxx;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Linq;
using System.Threading.Tasks;
using FourTwenty.IoT.Connect.Models;

namespace FourTwenty.IoT.Server.Components.Sensors
{
    public class DhtSensor : IoTComponent, ISensor
    {
        private DhtBase _sensor;

        public event EventHandler<ModuleResponseEventArgs> DataReceived;
        public int ActualPin => Pins.FirstOrDefault();

        public DhtSensor(int gpioPin, GpioController controller, IReadOnlyCollection<IRule> rules) : base(rules, new[] { gpioPin }, controller) { }
        public DhtSensor(int gpioPin, GpioController controller) : base(new[] { gpioPin }, controller) { }

        protected override void Initialize()
        {
            if (Gpio.IsPinOpen(ActualPin))
            {
                Gpio.ClosePin(ActualPin);
            }

            _sensor = new Dht11(ActualPin, PinNumberingScheme.Logical, Gpio);
        }

        public ValueTask<object> GetData()
        {
            ModuleResponse response = null;

            try
            {
                var tmp = _sensor.Temperature.DegreesCelsius;
                var hum = _sensor.Humidity.Value;

                if (tmp > 0 && hum > 0)
                {
                    response = new ModuleResponse(true, new DhtData(_sensor.Temperature.DegreesCelsius, _sensor.Humidity.Value));
                }
            }
            catch (Exception ex)
            {
                response = new ModuleResponse(false, null);
            }
            finally
            {
                DataReceived?.Invoke(this, new ModuleResponseEventArgs(response));
            }

            return new ValueTask<object>(response);

        }
    }
}
