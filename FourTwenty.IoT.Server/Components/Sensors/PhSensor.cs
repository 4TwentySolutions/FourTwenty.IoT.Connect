using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Connect.Data;
using FourTwenty.IoT.Connect.Interfaces;
using FourTwenty.IoT.Connect.Models;
using FourTwenty.IoT.Server.Components.Modules;
using Microsoft.Extensions.Logging;
using System;
using System.Device.Gpio;
using System.Linq;
using System.Threading.Tasks;

namespace FourTwenty.IoT.Server.Components.Sensors
{
    public class PhSensor : IoTComponent, ISensor
    {
        private double _temperature = 25.0;
        private double _acidVoltage = 2032.44;
        private double _neutralVoltage = 1500.0;

        public SensorReadType ReadType => SensorReadType.Analog;

        public int AnalogSensorReadChannel { get; set; }

        public event EventHandler<ModuleResponseEventArgs> DataReceived;

        public PhSensor(GpioController controller) : base(controller) { }

        public override async ValueTask Initialize()
        {
            IsInitialized = true;
        }

        public async ValueTask<ModuleResponse> GetData()
        {
            ModuleResponse response = null;

            try
            {
                PhData baseData = null;

                var reader = IoTRuntimeService.GetModuleByType(ComponentType.Mcp3008);

                if (reader is Mcp3008IoT mcp3008IoT)
                {
                    var value = await mcp3008IoT.ReadChannel(AnalogSensorReadChannel);

                    if (value != null && value.Any())
                    {
                        var val = value.Average(x => x.Voltage);
                        val = val * 1000;
                        var isSuccess = IsInRange(val);

                        baseData = new PhData(ConvertVoltage(val));
                    }
                }

                _logger?.LogInformation($"\n{nameof(PhSensor)}:\n {baseData?.Value}");

                response = new ModuleResponse(Id, baseData != null, baseData);
            }
            catch (Exception ex)
            {
                response = new ModuleResponse(Id, false, null, ex);
            }
            finally
            {
                DataReceived?.Invoke(this, new ModuleResponseEventArgs(response));
            }

            return response;
        }

        public async Task Calibration(double voltage)
        {
            if (voltage > 1322 && voltage < 1678)
            {
                //print(">>>Buffer Solution:7.0")
                _neutralVoltage = voltage;
            }

            if (voltage > 1854 && voltage < 2210)
            {
                //print (">>>Buffer Solution:4.0")
                _acidVoltage = voltage;
            }

        }

        private bool IsInRange(double voltage)
        {
            return voltage > _neutralVoltage && voltage < _acidVoltage;
        }


        private double ConvertVoltage(double voltage)
        {
            var slope = (7.0 - 4.0) / ((_neutralVoltage - 1500.0) / 3.0 - (_acidVoltage - 1500.0) / 3.0);
            var intercept = 7.0 - slope * (_neutralVoltage - 1500.0) / 3.0;
            var phValue = slope * (voltage - 1500.0) / 3.0 + intercept;

            phValue = Math.Round(phValue, 2);

            return phValue;
        }

    }
}
