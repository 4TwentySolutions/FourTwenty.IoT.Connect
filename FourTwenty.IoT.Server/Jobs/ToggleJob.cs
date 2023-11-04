using System.Device.Gpio;
using System.Threading.Tasks;
using FourTwenty.IoT.Connect.Models;
using FourTwenty.IoT.Server.Components;
using Quartz;

namespace FourTwenty.IoT.Server.Jobs
{
    public class ToggleJob : IJob
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
                var pinValue = component.ReadValue(rule.Pin.GetValueOrDefault());
                pinValue = pinValue == PinValue.High ? PinValue.Low : PinValue.High;
                component.SetValue(pinValue, rule.Pin.GetValueOrDefault());
            }
            else
            {
                foreach (var pin in component.Pins)
                {
                    var pinValue = component.ReadValue(pin);
                    pinValue = pinValue == PinValue.High ? PinValue.Low : PinValue.High;
                    component.SetValue(pinValue, pin);
                }
            }


            return Task.CompletedTask;
        }
    }
}
