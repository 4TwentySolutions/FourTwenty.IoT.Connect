using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using FourTwenty.IoT.Connect.Interfaces;
using FourTwenty.IoT.Connect.Models;
using FourTwenty.IoT.Server.Components.Sensors;
using FourTwenty.IoT.Server.Models;
using Iot.Device.Hcsr04;

namespace FourTwenty.IoT.Server.Components
{
	public class WaterTank : IoTComponent, ISensor
	{
		public AdditionalData DisplayData { get; set; }

		private Hcsr04 _sensor;

		public WaterTank(int triggerPin, int echoPin, GpioController controller, IReadOnlyCollection<IRule> rules) : base(rules, new[] { triggerPin, echoPin }, controller)
		{
			_sensor = new Hcsr04(controller, triggerPin, echoPin);
		}

		public WaterTank(int triggerPin, int echoPin, GpioController controller) : base(new[] { triggerPin, echoPin }, controller)
		{
			_sensor = new Hcsr04(controller, triggerPin, echoPin);
		}

		public event EventHandler<ModuleResponseEventArgs> DataReceived;

		protected override void Initialize() { }

		public ValueTask<object> GetData()
		{
			var data = new WaterTankData();

			if (_sensor.TryGetDistance(out var ds))
			{
				data.IsSuccess = true;
				data.Value = Math.Round(ds.Centimeters, 2);
				if (DisplayData != null &&
					DisplayData.IsPercents.GetValueOrDefault() &&
				    DisplayData.MaxValue != -1 &&
				    DisplayData.MinValue != -1)
				{
					if (data.Value < DisplayData.MaxValue)
					{
						data.ValueLine = "100%";
					}
					else
					{
						if (data.Value > DisplayData.MinValue)
						{
							data.ValueLine = "0%";
						}
						else
						{
							var v = (DisplayData.MaxValue - data.Value) / (DisplayData.MaxValue / DisplayData.MinValue);
							v = v > 0 ? 100 - v : 100 + v;

							data.ValueLine = Math.Round(v, 2) + "%";
						}
					}
				}
				else
				{
					data.ValueLine = data.Value + "cm";
				}
				
			}
			else
			{
				data.IsSuccess = false;
			}

			DataReceived?.Invoke(this, new ModuleResponseEventArgs(data));

			return new ValueTask<object>(data);
		}
	}
}
