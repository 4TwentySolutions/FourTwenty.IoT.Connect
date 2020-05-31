using System;
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

        public event EventHandler<SensorEventArgs> DataReceived;

        public ValueTask<object> GetData()
        {
            if (!_sensor.IsLastReadSuccessful)
                return new ValueTask<object>(null);

            var data = new DhtData(_sensor.Temperature.Celsius, _sensor.Humidity);

            DataReceived?.Invoke(this, new SensorEventArgs(data));

            return new ValueTask<object>(data);
        }
    }

    public class DhtData
    {
        public DhtData() { }

        public DhtData(double temp, double humidity)
        {
            Temperature = temp;
            Humidity = humidity;
        }

        public double Temperature { get; set; }
        public double Humidity { get; set; }

        /// <summary>
        /// Return DHT sensor values:
        ///     - Temperature (celsius)
        ///     - Humidity
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{nameof(Temperature)}: {Temperature}\n{nameof(Humidity)}: {Humidity}";
        }
    }
}
