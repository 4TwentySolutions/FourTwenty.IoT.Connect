using System;
using FourTwenty.IoT.Connect.Interfaces;
using Iot.Device.DHTxx;
using System.Device.Gpio;
using System.Linq;
using System.Threading.Tasks;
using FourTwenty.IoT.Connect.Models;
using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Connect.Data;

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

        public override ValueTask Initialize()
        {
            try
            {
                if (Gpio.IsPinOpen(ActualPin))
                {
                    Gpio.ClosePin(ActualPin);
                }

                if (DhtType == DhtType.Dht11)
                {
                    _sensor = new Dht11(ActualPin, PinNumberingScheme.Logical, Gpio);
                }
                if (DhtType == DhtType.Dht22)
                {
                    _sensor = new Dht22(ActualPin, PinNumberingScheme.Logical, Gpio);
                }

                return ValueTask.CompletedTask;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public ValueTask<ModuleResponse> GetData()
        {
           ModuleResponse response = null;            

            try
            {
                var tryReadTemp = _sensor.TryReadTemperature(out var tmp);
                var tryReadHmd = _sensor.TryReadHumidity(out var hmd);

                response = new ModuleResponse(Id, tryReadTemp && tryReadHmd, new DhtData(Math.Round(tmp.DegreesCelsius, 2), Math.Round(hmd.Value, 2)));

            }
            catch (Exception ex)
            {
                response = new ModuleResponse(Id, false, new DhtData(), ex);
            }
            finally
            {
                DataReceived?.Invoke(this, new ModuleResponseEventArgs(response));
            }

            return new ValueTask<ModuleResponse>(response);
        }
    }
}
