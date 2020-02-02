using FourTwenty.IoT.Connect.Common;
using FourTwenty.IoT.Connect.Interfaces;
using Iot.Device.DHTxx;
using System;
using System.Device.Gpio;
using System.Linq;
using System.Threading.Tasks;

namespace FourTwenty.IoT.Server.Components.Sensors
{
    public class DhtSensor : IoTComponent, ISensor<Dht11>
    {
        private readonly Dht11 _sensor;
        public DhtSensor(int gpioPin, GpioController controller) : base(new[] { gpioPin }, controller)
        {
            _sensor = new Dht11(gpioPin);
        }

        public int ActivePin => Pins.FirstOrDefault();


        public ValueTask<Dht11> GetData()
        {
            return new ValueTask<Dht11>(_sensor);
        }
    }
}
