using System.Collections.Generic;
using System.Threading.Tasks;
using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Connect.Interfaces;
using FourTwenty.IoT.Connect.Models;
using FourTwenty.IoT.Server.Components;
using FourTwenty.IoT.Server.Components.Modules;
using FourTwenty.IoT.Server.Extensions;
using FourTwenty.IoT.Server.Interfaces;
using Quartz;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FourTwenty.IoT.Server.Jobs
{
    public class ReadJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            IoTComponent component = null;
            BaseRule rule = null;

            if (context.JobDetail.JobDataMap.TryGetValue(JobsKeys.ComponentKey, out var rawObj))
                component = rawObj as IoTComponent;

            if (context.JobDetail.JobDataMap.TryGetValue(JobsKeys.RuleKey, out var rawRule))
                rule = rawRule as BaseRule;

            if (component == null || rule == null)
                return;

            if (component.ComponentType == ComponentType.SoilMoisture)
            {

            }

            await component.Actions.ExecuteActions(ActionType.Pre);

            ModuleResponse data = null;

            switch (component)
            {
                case ISensor sensor:
                    {
                        data = await sensor.GetData(new Dictionary<string, object>
                        {
                            {"RuleId", rule.Id}
                        });

                        //await component.Actions.ExecuteActions(ActionType.Comparison, data);
                        break;
                    }
                case Camera camera:
                    {
                        data = await camera.GetPhoto();
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
