using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Connect.Interfaces;
using FourTwenty.IoT.Connect.Models;
using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Text;
using System.Threading.Tasks;

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
            throw new NotImplementedException();
        }


    }
}
