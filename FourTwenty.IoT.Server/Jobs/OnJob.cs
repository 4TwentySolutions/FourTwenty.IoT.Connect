using System.Device.Gpio;
using System.Threading.Tasks;
using FourTwenty.IoT.Connect.Models;
using FourTwenty.IoT.Server.Components;
using Quartz;

namespace FourTwenty.IoT.Server.Jobs
{
    public class OnJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            IoTComponent component = null;
            BaseRule rule = null;
            
            if (context.JobDetail.JobDataMap.TryGetValue(JobsKeys.ComponentKey, out var rawObj))
                component = rawObj as IoTComponent;
            if (context.JobDetail.JobDataMap.TryGetValue(JobsKeys.RuleKey, out var rawRule))
                rule = rawRule as BaseRule;

            
            if (component == null)
                return Task.CompletedTask;

            if (rule?.Pin != null)
            {
                component.SetValue(PinValue.Low, rule.Pin.GetValueOrDefault());
            }
            else
            {
                foreach (var pin in component.Pins)
                {
                    component.SetValue(PinValue.Low, pin);
                }
            }

            return Task.CompletedTask;
        }
    }
}
