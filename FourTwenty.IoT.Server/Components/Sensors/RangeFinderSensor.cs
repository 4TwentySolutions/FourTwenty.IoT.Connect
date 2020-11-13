using FourTwenty.IoT.Connect.Interfaces;
using FourTwenty.IoT.Server.Models;
using Iot.Device.Hcsr04;
using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Diagnostics;
using System.Threading.Tasks;

namespace FourTwenty.IoT.Server.Components.Sensors
{
    public class RangeFinderSensor : IoTComponent, ISensor
    {
        private Hcsr04 _sensor = null;

        public RangeFinderSensor(int triggerPin, int echoPin, GpioController controller, IReadOnlyCollection<IRule> rules) : base(rules, new[] { triggerPin, echoPin }, controller)
        {
            _sensor = new Hcsr04(controller, triggerPin, echoPin);
        }
        public RangeFinderSensor(int triggerPin, int echoPin, GpioController controller) : base(new[] { triggerPin, echoPin }, controller)
        {
            _sensor = new Hcsr04(controller, triggerPin, echoPin);
        }

        public event EventHandler<SensorEventArgs> DataReceived;

        protected override void Initialize() { }

        public ValueTask<object> GetData()
        {
            var ds = _sensor.Distance.Centimeters;

            var data = new RangeFinderData(Math.Round(ds, 2));

            Debug.Print("Distance:" + ds.ToString());

            DataReceived?.Invoke(this, new SensorEventArgs(data));

            return new ValueTask<object>(data);
        }
    }
}
