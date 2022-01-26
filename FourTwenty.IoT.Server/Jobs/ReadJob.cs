using System.Linq;
using System.Threading.Tasks;
using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Connect.Interfaces;
using FourTwenty.IoT.Server.Components;
using FourTwenty.IoT.Server.Components.Modules;
using FourTwenty.IoT.Server.Components.Sensors;
using FourTwenty.IoT.Server.Extensions;
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

            if (component == null)
                return;

            await component.Actions.ExecuteActions(ActionType.Pre);

            switch (component)
            {
                case ISensor sensor:
                {
                    var data = await sensor.GetData();

                    if (messagesService != null)
                        await messagesService.SendMessage(component, data);

                    await component.Actions.ExecuteActions(ActionType.Comparison, data);
                    break;
                }
                case Camera camera:
                {
                    var data = await camera.GetPhoto();

                    if (messagesService != null)
                        await messagesService.SendMessage(component, data);
                    break;
                }
                case Mcp3008IoT mcp3008:
                {

                    break;
                }
            }

            await component.Actions.ExecuteActions(ActionType.Post);
        }
    }
}
