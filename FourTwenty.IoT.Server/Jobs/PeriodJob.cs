using System;
using System.Threading.Tasks;
using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Connect.Models;
using FourTwenty.IoT.Server.Publishers;
using GrowIoT.MessageQueue.Enums;
using GrowIoT.MessageQueue.Interfaces;
using Quartz;

namespace FourTwenty.IoT.Server.Jobs
{
    public class PeriodJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            int? componentId = null;
            int? ruleId = null;
            int? pin = null;
            TimeSpan? period = null;
            IBasicPublisher<ComponentJobMessage> publisher = null;

            if (context.JobDetail.JobDataMap.TryGetValue(JobsKeys.ComponentIdKey, out var rawObj))
                componentId = (int)rawObj;

            if (context.JobDetail.JobDataMap.TryGetValue(JobsKeys.RuleIdKey, out var rawRule))
                ruleId = (int)rawRule;

            if (context.JobDetail.JobDataMap.TryGetValue(JobsKeys.PinKey, out var rawPin))
                pin = (int)rawPin;

            if (context.JobDetail.JobDataMap.TryGetValue(JobsKeys.PeriodKey, out var rawPeriod))
                period = (TimeSpan)rawPeriod;

            if (context.JobDetail.JobDataMap.TryGetValue(JobsKeys.JobPublishKey, out var rawPublisher))
                publisher = rawPublisher as JobsPublisher;


            if (componentId == null || ruleId == null || publisher == null)
                return;

            publisher.Publish(new ComponentJobMessage
            {
                ComponentId = componentId.GetValueOrDefault(),
                RuleId = ruleId.GetValueOrDefault(),
                Command = Commands.Period,
                Pin = pin,
                Period = period
            }, $"component_{componentId}_jobs", MessagePriority.Default);
        }
    }
}
