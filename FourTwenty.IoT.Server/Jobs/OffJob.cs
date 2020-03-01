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
            IoTComponent component = null;
            
            if (context.JobDetail.JobDataMap.TryGetValue(JobsKeys.ComponentKey, out var rawObj))
                component = rawObj as IoTComponent;
            
            if (component == null)
                return Task.CompletedTask;

            foreach (var pin in component.Pins)
            {
                component.SetValue(PinValue.High, pin);
            }
            return Task.CompletedTask;
        }
    }
}
