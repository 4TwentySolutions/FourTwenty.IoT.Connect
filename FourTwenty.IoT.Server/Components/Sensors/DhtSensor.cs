using FourTwenty.IoT.Connect.Interfaces;
using Iot.Device.DHTxx;
using System.Device.Gpio;
using System.Linq;
using System.Threading.Tasks;
using Iot.Units;

namespace FourTwenty.IoT.Server.Components.Sensors
{
    public class DhtSensor : IoTComponent, ISensor<DhtData>
    {
        private readonly Dht11 _sensor;
        public DhtSensor(int gpioPin, GpioController controller) : base(new[] { gpioPin }, controller)
        {
            _sensor = new Dht11(gpioPin);
        }

        public int ActivePin => Pins.FirstOrDefault();
        
        public ValueTask<DhtData> GetData()
        {
            return new ValueTask<DhtData>(new DhtData(_sensor.Temperature, _sensor.Humidity));
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
