using System.Device.Gpio;
using System.Threading.Tasks;
using FourTwenty.IoT.Server.Components;
using Quartz;

namespace FourTwenty.IoT.Server.Jobs
{
    public class OnJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            if (!(context.Get(JobsKeys.ComponentKey) is IoTComponent component))
                return Task.CompletedTask;

            foreach (var pin in component.Pins)
            {
                component.SetValue(PinValue.Low, pin);
            }

            return Task.CompletedTask;
        }
    }
}
