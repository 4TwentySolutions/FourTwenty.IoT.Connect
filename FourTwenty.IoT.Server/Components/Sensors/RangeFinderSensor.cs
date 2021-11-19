using FourTwenty.IoT.Connect.Interfaces;
using Iot.Device.Hcsr04;
using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Connect.Extensions;
using FourTwenty.IoT.Connect.Models;

namespace FourTwenty.IoT.Server.Components.Sensors
{
	public class RangeFinderSensor : IoTComponent, ISensor
	{
		private Hcsr04 _sensor;
		private bool isDisposed;

		public RangeFinderSensor(int triggerPin, int echoPin, GpioController controller, IReadOnlyCollection<IRule> rules) : base(rules, new[] { triggerPin, echoPin }, controller)
		{
            ComponentType = ComponentType.RangeFinder;
        }

		public RangeFinderSensor(int triggerPin, int echoPin, GpioController controller) : base(new[] { triggerPin, echoPin }, controller)
		{
            ComponentType = ComponentType.RangeFinder;
        }

		public event EventHandler<ModuleResponseEventArgs> DataReceived;

        public override void Initialize()
        {
            try
            {
                foreach (var pin in Pins)
                {
                    Gpio.ClosePin(pin);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            _sensor = new Hcsr04(Gpio, Pins.FirstOrDefault(), Pins.LastOrDefault());
        }

		public ValueTask<object> GetData()
        {
            RangeFinderData data = null;

            if (_sensor.TryGetDistance(out var ds))
            {
                data = new RangeFinderData(Math.Round(ds.Centimeters, 2));
            }

			var dpData =  data.ApplyDisplayOptions(DisplayOptions, ComponentType);

			DataReceived?.Invoke(this, new ModuleResponseEventArgs(new ModuleResponse<IData>(dpData != null, dpData)));

			return new ValueTask<object>(dpData);
		}
    }
}
