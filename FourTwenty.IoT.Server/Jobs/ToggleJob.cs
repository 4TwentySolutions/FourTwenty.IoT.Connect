using System.Device.Gpio;
using System.Threading.Tasks;
using FourTwenty.IoT.Server.Components;
using Quartz;

namespace FourTwenty.IoT.Server.Jobs
{
    public class ToggleJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            if (!(context.Get(JobsKeys.ComponentKey) is IoTComponent component))
                return Task.CompletedTask;
            foreach (var pin in component.Pins)
            {
                var pinValue = component.ReadValue(pin);
                pinValue = pinValue == PinValue.High ? PinValue.Low : PinValue.High;
                component.SetValue(pinValue, pin);
            }
            return Task.CompletedTask;
        }
    }
}
