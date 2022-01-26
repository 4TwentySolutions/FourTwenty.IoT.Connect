using System;
using FourTwenty.IoT.Connect.Interfaces;
using Iot.Device.DHTxx;
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
        public SensorReadType ReadType { get; set; }

        public DhtType DhtType { get; set; }
        
        public DhtSensor(PinNameItem gpioPin, GpioController controller) : base(new[] { gpioPin }, controller) { }

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

        public ValueTask<ModuleResponse<BaseData>> GetData()
        {
            ModuleResponse<BaseData> response = null;            

            try
            {
                var tmp = _sensor.Temperature.DegreesCelsius;
                var hmd = _sensor.Humidity.Value;

                response = _sensor == null ?
                    new ModuleResponse<BaseData>(false, null) :
                    new ModuleResponse<BaseData>(_sensor.Temperature.DegreesCelsius > -150, new DhtData(Math.Round(_sensor.Temperature.DegreesCelsius, 2), Math.Round(_sensor.Humidity.Value, 2)));
            }
            catch (Exception ex)
            {
                response = new ModuleResponse<BaseData>(false, null, ex);
            }
            finally
            {
                DataReceived?.Invoke(this, new ModuleResponseEventArgs(response));
            }

            return new ValueTask<ModuleResponse<BaseData>>(response);
        }
    }
}
