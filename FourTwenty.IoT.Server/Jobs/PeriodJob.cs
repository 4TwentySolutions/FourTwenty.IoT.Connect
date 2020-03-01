using System;
using System.Device.Gpio;
using System.Threading.Tasks;
using FourTwenty.IoT.Connect.Interfaces;
using FourTwenty.IoT.Server.Components;
using Quartz;

namespace FourTwenty.IoT.Server.Jobs
{
    public class PeriodJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            IoTComponent component = null;
            IPeriodRule rule = null;
            
            if (context.JobDetail.JobDataMap.TryGetValue(JobsKeys.ComponentKey, out var rawObj))
                component = rawObj as IoTComponent;

            if (context.JobDetail.JobDataMap.TryGetValue(JobsKeys.RuleKey, out var rawRule))
                rule = rawRule as IPeriodRule;
            
            if (component == null || rule == null)
                return Task.CompletedTask;
            
            foreach (var pin in component.Pins)
            {
                TriggerPeriod(component, pin, rule.Period);
            }
            return Task.CompletedTask;
        }

        private async void TriggerPeriod(IoTComponent component, int pin, TimeSpan period)
        {
            component.SetValue(PinValue.Low, pin);
            await Task.Delay(period);
            component.SetValue(PinValue.High, pin);
        }
    }
}
