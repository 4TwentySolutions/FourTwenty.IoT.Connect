using System;
using FourTwenty.IoT.Connect.Interfaces;
using Iot.Device.DHTxx;
using System.Device.Gpio;
using System.Linq;
using System.Threading.Tasks;
using FourTwenty.IoT.Connect.Models;
using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Connect.Data;
using GrowIoT.MessageQueue.Base;

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

        protected override async void OnMessageConsumerReceived(object sender, MessageEventArgs<ComponentJobMessage> e)
        {
            base.OnMessageConsumerReceived(sender, e);
            
            var response = await GetData();

            if (response != null)
            {
                response.RuleId = e.Message.RuleId;
            }

            MessageConsumer.Ack(e.DeliveryTag);
            DataReceived?.Invoke(this, new ModuleResponseEventArgs(response));
        }

        public ValueTask<ModuleResponse> GetData()
        {
            ModuleResponse response = null;

            try
            {
                var tryReadTemp = _sensor.TryReadTemperature(out var tmp);
                var tryReadHmd = _sensor.TryReadHumidity(out var hmd);

                var success = tryReadTemp && tryReadHmd;
                var result = new DhtData(Math.Round(tmp.DegreesCelsius, 2), Math.Round(hmd.Value, 2));
                if (success && result.Temperature < -100)
                    success = false;

                response = new ModuleResponse(Id, success, result);

            }
            catch (Exception ex)
            {
                response = new ModuleResponse(Id, false, new DhtData(), ex);
            }

            return new ValueTask<ModuleResponse>(response);
        }
    }
}
