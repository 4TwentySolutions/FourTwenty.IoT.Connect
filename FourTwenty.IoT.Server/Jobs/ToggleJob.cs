using System.Device.Gpio;
using System.Threading.Tasks;
using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Connect.Models;
using FourTwenty.IoT.Server.Components;
using FourTwenty.IoT.Server.Publishers;
using GrowIoT.MessageQueue.Enums;
using GrowIoT.MessageQueue.Interfaces;
using Quartz;

namespace FourTwenty.IoT.Server.Jobs
{
    public class ToggleJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            int? componentId = null;
            int? ruleId = null;
            int? pin = null;
            IBasicPublisher<ComponentJobMessage> publisher = null;

            if (context.JobDetail.JobDataMap.TryGetValue(JobsKeys.ComponentIdKey, out var rawObj))
                componentId = (int)rawObj;

            if (context.JobDetail.JobDataMap.TryGetValue(JobsKeys.RuleIdKey, out var rawRule))
                ruleId = (int)rawRule;

            if (context.JobDetail.JobDataMap.TryGetValue(JobsKeys.PinKey, out var rawPin))
                pin = (int)rawPin;

            if (context.JobDetail.JobDataMap.TryGetValue(JobsKeys.JobPublishKey, out var rawPublisher))
                publisher = rawPublisher as JobsPublisher;


            if (componentId == null || ruleId == null || publisher == null)
                return;

            publisher.Publish(new ComponentJobMessage
            {
                ComponentId = componentId.GetValueOrDefault(),
                RuleId = ruleId.GetValueOrDefault(),
                Command = Commands.Toggle,
                Pin = pin
            }, $"component_{componentId}_jobs", MessagePriority.Default);
        }
    }
}
