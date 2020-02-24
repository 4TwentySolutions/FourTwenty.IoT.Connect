using System.Threading.Tasks;
using FourTwenty.IoT.Connect.Interfaces;
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
            if (!(context.Get(JobsKeys.MessagesKey) is IMessagesService messagesService))
                return;
            if (component is ISensor sensor)
            {
                var data = await sensor.GetData();

                await messagesService.SendMessage(component, data);
            }
        }
    }
}
