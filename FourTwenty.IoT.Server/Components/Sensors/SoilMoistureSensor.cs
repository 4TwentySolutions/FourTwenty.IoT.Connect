using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Connect.Interfaces;
using FourTwenty.IoT.Connect.Models;
using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FourTwenty.IoT.Server.Components.Modules;
using FourTwenty.IoT.Server.Extensions;
using Microsoft.Extensions.Logging;
using FourTwenty.IoT.Connect.Data;

namespace FourTwenty.IoT.Server.Components.Sensors
{
    public class SoilMoistureSensor : IoTComponent, ISensor
    {
        private const double Min = 1.5; // 100% wet
        private const double Max = 3.3; // 0% wet or no signal

        public SensorReadType ReadType { get; set; }
        public int AnalogSensorReadChannel { get; set; }

        public event EventHandler<ModuleResponseEventArgs> DataReceived;

        public SoilMoistureSensor(IReadOnlyCollection<PinNameItem> pins, GpioController controller) : base(pins, controller) { }

        public override ValueTask Initialize()
        {
            foreach (var pin in Pins)
            {
                if (Gpio.IsPinOpen(pin))
                    Gpio.ClosePin(pin);
            }

            return ValueTask.CompletedTask;
        }

        public async ValueTask<ModuleResponse> GetData(Dictionary<string, object> additionalParams)
        {
            ModuleResponse response = null;

            try
            {
                SoilMoistureData baseData = null;

                switch (ReadType)
                {
                    case SensorReadType.Digital:
                        {
                            RelayState? value = ReadValue(Pins.FirstOrDefault()).GetState();

                            baseData = new SoilMoistureData
                            {
                                Value = value.ToString(),
                                Moisture = value == RelayState.Opened ? 1 : 0
                            };
                            break;
                        }
                    case SensorReadType.Analog:

                        var reader = IoTRuntimeService.GetModuleByType(ComponentType.Mcp3008);

                        if (reader is Mcp3008IoT mcp3008IoT)
                        {
                            var value = await mcp3008IoT.ReadChannel(AnalogSensorReadChannel);

                            if (value != null && value.Any())
                            {
                                var val = value.Average(x => x.Voltage);
                                val = Math.Round(val, 2);

                                if (val > Min && val < Max)
                                {
                                    var percentage = 1 - ((val - Min) / (Max - Min));

                                    var res = Math.Round(percentage * 100, 2);

                                    baseData = new SoilMoistureData
                                    {
                                        Value = res + "%",
                                        Moisture = res
                                    };
                                }
                            }
                        }

                        break;
                }

                _logger?.LogInformation($"\n{nameof(SoilMoistureSensor)}:\n {baseData?.Value}");

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


    }
}
