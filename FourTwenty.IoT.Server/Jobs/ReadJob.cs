using System.Threading.Tasks;
using FourTwenty.IoT.Server.Components;
using FourTwenty.IoT.Server.Components.Sensors;
using FourTwenty.IoT.Server.Interfaces;
using Quartz;

namespace FourTwenty.IoT.Server.Jobs
{
    public class ReadJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            if (!(context.Get(JobsKeys.ComponentKey) is IoTComponent component))
                return;
            if (!(context.Get(JobsKeys.HubKey) is IHubService hubService))
                return;
            if (component is DhtSensor sensor)
            {
                var data = await sensor.GetData();
                //TODO Finish signalR
                await hubService.SendMessage(string.Empty, data);
            }
        }
    }
}
