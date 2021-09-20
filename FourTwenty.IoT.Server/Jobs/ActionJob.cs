using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FourTwenty.IoT.Connect.Interfaces;
using FourTwenty.IoT.Server.Components;
using Quartz;

namespace FourTwenty.IoT.Server.Jobs
{
    public class ActionJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            IoTComponent component = null;
            IRule rule = null;

            if (context.JobDetail.JobDataMap.TryGetValue(JobsKeys.ComponentKey, out var rawObj))
                component = rawObj as IoTComponent;

            if (context.JobDetail.JobDataMap.TryGetValue(JobsKeys.RuleKey, out var rawRule))
                rule = rawRule as IRule;


            if (component == null)
                return;

            if (component is ISensor sensor)
            {
                var data = await sensor.GetData();
                


            }
        }
    }
}
