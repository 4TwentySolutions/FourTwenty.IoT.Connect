using FourTwenty.IoT.Connect.Interfaces;
using FourTwenty.IoT.Server.Models;
using Iot.Device.Hcsr04;
using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Diagnostics;
using System.Threading.Tasks;
using FourTwenty.IoT.Connect.Models;

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

        public event EventHandler<ModuleResponseEventArgs> DataReceived;

        protected override void Initialize() { }

        public ValueTask<object> GetData()
        {
	        var data = _sensor.TryGetDistance(out var ds) ? 
		        new RangeFinderData(true,Math.Round(ds.Centimeters, 2)) :
		        new RangeFinderData(false);
            
            Debug.Print("Distance:" + ds.Centimeters);

            DataReceived?.Invoke(this, new ModuleResponseEventArgs(data));

            return new ValueTask<object>(data);
        }
    }
}
