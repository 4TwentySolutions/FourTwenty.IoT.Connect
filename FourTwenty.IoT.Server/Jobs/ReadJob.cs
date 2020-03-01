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
            IoTComponent component = null;
            IMessagesService messagesService = null;
            
            if (context.JobDetail.JobDataMap.TryGetValue(JobsKeys.ComponentKey, out var rawObj))
                component = rawObj as IoTComponent;
            
            if (context.JobDetail.JobDataMap.TryGetValue(JobsKeys.MessagesKey, out var rawMessagesService))
                messagesService = rawMessagesService as IMessagesService;
            
            if (component == null || messagesService == null)
                return;
            
            if (component is ISensor sensor)
            {
                var data = await sensor.GetData();

                await messagesService.SendMessage(component, data);
            }
        }
    }
}
