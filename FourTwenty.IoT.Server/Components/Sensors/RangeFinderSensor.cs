using FourTwenty.IoT.Connect.Interfaces;
using Iot.Device.Hcsr04;
using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Connect.Models;
using FourTwenty.IoT.Server.Extensions;
using FourTwenty.IoT.Connect.Data;
using FourTwenty.IoT.Server.Rules;

namespace FourTwenty.IoT.Server.Components.Sensors
{
	public class RangeFinderSensor : IoTComponent, ISensor
	{
		private Hcsr04 _sensor;
		private bool isDisposed;

		public RangeFinderSensor(PinNameItem triggerPin, PinNameItem echoPin, GpioController controller, IReadOnlyCollection<CronRule> rules) : base(rules, new[] { triggerPin, echoPin }, controller)
		{
            ComponentType = ComponentType.RangeFinder;
        }

		public RangeFinderSensor(PinNameItem triggerPin, PinNameItem echoPin, GpioController controller) : base(new[] { triggerPin, echoPin }, controller)
		{
            ComponentType = ComponentType.RangeFinder;
        }

		public event EventHandler<ModuleResponseEventArgs> DataReceived;

        public override ValueTask Initialize()
        {
            foreach (var pin in Pins)
            {
                if (Gpio.IsPinOpen(pin))
                    Gpio.ClosePin(pin);
            }

            _sensor = new Hcsr04(Gpio, Pins.FirstOrDefault(), Pins.LastOrDefault());

            return ValueTask.CompletedTask;
        }

		public ValueTask<ModuleResponse> GetData()
        {
            RangeFinderData data = null;

            foreach (var pin in Pins)
            {
                if (!Gpio.IsPinOpen(pin))
                    Gpio.OpenPin(pin);
            }

            if (_sensor.TryGetDistance(out var ds))
            {
                data = new RangeFinderData(Math.Round(ds.Centimeters, 2));
            }

			var dpData =  data.ApplyDisplayOptions(DisplayOptions, ComponentType);

            var response = new ModuleResponse(Id, dpData != null, dpData);

			DataReceived?.Invoke(this, new ModuleResponseEventArgs(response));

            return new ValueTask<ModuleResponse>(response);
        }

        public SensorReadType ReadType { get; set; }
    }
}
