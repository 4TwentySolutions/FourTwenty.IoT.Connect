using System;
using FourTwenty.IoT.Connect.Interfaces;
using Iot.Device.DHTxx;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Linq;
using System.Threading.Tasks;
using FourTwenty.IoT.Connect.Models;
using FourTwenty.IoT.Connect.Constants;

namespace FourTwenty.IoT.Server.Components.Sensors
{
    public class DhtSensor : IoTComponent, ISensor
    {
        private DhtBase _sensor;

        public event EventHandler<ModuleResponseEventArgs> DataReceived;
        public int ActualPin => Pins.FirstOrDefault();

        public DhtType DhtType { get; set; }

        public DhtSensor(int gpioPin, GpioController controller, IReadOnlyCollection<IRule> rules) : base(rules, new[] { gpioPin }, controller) { }
        public DhtSensor(int gpioPin, GpioController controller) : base(new[] { gpioPin }, controller) { }

        public override void Initialize()
        {
            if (Gpio.IsPinOpen(ActualPin))
            {
                Gpio.ClosePin(ActualPin);
            }

            if(DhtType == DhtType.Dht11)
            {
                _sensor = new Dht11(ActualPin, PinNumberingScheme.Logical, Gpio);
            }
            if (DhtType == DhtType.Dht22)
            {
                _sensor = new Dht22(ActualPin, PinNumberingScheme.Logical, Gpio);
            }            
        }

        public ValueTask<object> GetData()
        {
            ModuleResponse response = null;            

            try
            {
                if (_sensor == null)
                {
                    response = new ModuleResponse(false, null);
                }
                else
                {
                    var tmp = _sensor.Temperature.DegreesCelsius;
                    var hum = _sensor.Humidity.Value;

                    response = new ModuleResponse(_sensor.Temperature.DegreesCelsius < -150 ? false : true, new DhtData(Math.Round(_sensor.Temperature.DegreesCelsius, 2), Math.Round(_sensor.Humidity.Value, 2)));                    
                }                
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
