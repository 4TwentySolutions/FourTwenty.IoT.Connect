using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Connect.Interfaces;
using FourTwenty.IoT.Connect.Models;
using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FourTwenty.IoT.Server.Extensions;

namespace FourTwenty.IoT.Server.Components.Sensors
{
    public class SoilMoistureSensor : IoTComponent, ISensor
    {
        public SensorReadType SensorReadType { get; set; }

        public event EventHandler<ModuleResponseEventArgs> DataReceived;


        public SoilMoistureSensor(int gpioPin, GpioController controller, IReadOnlyCollection<IRule> rules) : base(rules, new[] { gpioPin }, controller) { }
        public SoilMoistureSensor(int gpioPin, GpioController controller) : base(new[] { gpioPin }, controller) { }

        public override void Initialize() { }

        public ValueTask<object> GetData()
        {

            ModuleResponse response = null;

            try
            {
                var value = ReadValue(Pins.FirstOrDefault()).GetState();

                var data = new SoilMoistureData()
                {
                    Value = value.ToString(),
                    State = value
                };

                response = new ModuleResponse(true, data);

                Debug.Write($"{nameof(SoilMoistureSensor)}: {value.ToString()}");
            }
            catch (Exception ex)
            {
                response = new ModuleResponse(false, null);
            }
            finally
            {
                DataReceived?.Invoke(this, new ModuleResponseEventArgs(response));
            }


            return new ValueTask<object>(response);
        }


    }
}
