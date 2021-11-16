using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Connect.Interfaces;
using FourTwenty.IoT.Connect.Models;
using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FourTwenty.IoT.Server.Extensions;

namespace FourTwenty.IoT.Server.Components.Sensors
{
    public class SoilMoistureSensor : IoTComponent, ISensor
    {
        public SensorReadType SensorReadType { get; set; }
        public event EventHandler<ModuleResponseEventArgs> DataReceived;

        public SoilMoistureSensor(IReadOnlyCollection<int> pins, GpioController controller, IReadOnlyCollection<IRule> rules) : base(rules, pins, controller) { }
        public SoilMoistureSensor(IReadOnlyCollection<int> pins, GpioController controller) : base(pins, controller) { }

        public override void Initialize() { }

        public ValueTask<object> GetData()
        {
            ModuleResponse response = null;

            try
            {
                Dictionary<int, RelayState> dict = null;
                RelayState? value = null;

                if (GroupedModule)
                {
                    dict = Pins.ToDictionary(pin => pin, pin => ReadValue(pin).GetState());
                }
                else
                {
                    value = ReadValue(Pins.FirstOrDefault()).GetState();
                }

                var data = new SoilMoistureData
                {
                    Value = value.HasValue ? value.ToString() : string.Join(Environment.NewLine, dict),
                    State = value,
                    Values = dict
                };

                Debug.Write($"\n{nameof(SoilMoistureSensor)}:\n {data.Value}");

                response = new ModuleResponse(true, data);
            }
            catch (Exception ex)
            {
                response = new ModuleResponse(false, null, ex);
            }
            finally
            {
                DataReceived?.Invoke(this, new ModuleResponseEventArgs(response));
            }


            return new ValueTask<object>(response);
        }


    }
}
