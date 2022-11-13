using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Connect.Interfaces;
using FourTwenty.IoT.Connect.Models;
using FourTwenty.IoT.Server.Components.Modules;
using FourTwenty.IoT.Server.Extensions;
using GrowIoT.Rules;

namespace FourTwenty.IoT.Server.Components.Sensors
{
    public class NonContactLiquidSensor : IoTComponent, ISensor
    {
        public NonContactLiquidSensor(IReadOnlyCollection<PinNameItem> pins, GpioController gpioController) : base(pins, gpioController)
        {
        }

        protected NonContactLiquidSensor(IReadOnlyCollection<CronRule> rules, IReadOnlyCollection<PinNameItem> pins, GpioController gpioController) : base(rules, pins, gpioController)
        {
        }

        public event EventHandler<ModuleResponseEventArgs> DataReceived;
        public async ValueTask<ModuleResponse<BaseData>> GetData()
        {
            ModuleResponse<BaseData> response = null;

            try
            {
                var value = ReadValue(Pins.FirstOrDefault());

                Debug.Write($"\n{nameof(NonContactLiquidSensor)}:\n {value}");

                response = new ModuleResponse<BaseData>(true, new PinValueData(value == PinValue.High ? 1 : 0));
            }
            catch (Exception ex)
            {
                response = new ModuleResponse<BaseData>(false, null, ex);
            }
            finally
            {
                DataReceived?.Invoke(this, new ModuleResponseEventArgs(response));
            }

            return response;
            
        }

        public SensorReadType ReadType => SensorReadType.Digital;
    }
}
