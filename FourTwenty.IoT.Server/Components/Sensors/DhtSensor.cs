using FourTwenty.IoT.Connect.Interfaces;
using Iot.Device.DHTxx;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Linq;
using System.Threading.Tasks;
using Iot.Device.GrovePiDevice.Sensors;
using Iot.Units;

namespace FourTwenty.IoT.Server.Components.Sensors
{
    public class DhtSensor : IoTComponent, ISensor
    {
        private readonly Dht11 _sensor;

        public DhtSensor(int gpioPin, GpioController controller, IReadOnlyCollection<IRule> rules) : base(rules, new[] { gpioPin }, controller)
        {
            _sensor = new Dht11(gpioPin);
        }
        public DhtSensor(int gpioPin, GpioController controller) : base(new[] { gpioPin }, controller)
        {
            _sensor = new Dht11(gpioPin);
        }

        public int ActivePin => Pins.FirstOrDefault();

        public ValueTask<object> GetData()
        {
            return new ValueTask<object>(new DhtData(_sensor.Temperature, _sensor.Humidity));
        }
    }

    public class DhtData
    {
        public DhtData(Temperature temp, double humidity)
        {
            Temperature = temp;
            Humidity = humidity;
        }

        public Temperature Temperature { get; }
        public double Humidity { get; }
    }
}
