using System;
using System.Device.Gpio;
using System.Threading.Tasks;
using FourTwenty.IoT.Server.Components;
using Quartz;

namespace FourTwenty.IoT.Server.Jobs
{
    public class OffJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            if (!(context.Get(JobsKeys.ComponentKey) is IoTComponent component))
                return Task.CompletedTask;

            foreach (var pin in component.Pins)
            {
                component.SetValue(PinValue.High, pin);
            }
            return Task.CompletedTask;
        }
    }
}
