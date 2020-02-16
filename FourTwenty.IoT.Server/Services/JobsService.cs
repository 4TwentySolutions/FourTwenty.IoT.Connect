using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FourTwenty.IoT.Connect.Interfaces;
using FourTwenty.IoT.Server.Interfaces;
using Quartz.Impl;

namespace FourTwenty.IoT.Server.Services
{
    public class JobsService : IJobsService
    {
        #region fields

        private readonly IHubService _hubService;

        #endregion

        public JobsService(IHubService hubService)
        {
            _hubService = hubService;
        }
        public async Task StartJobs(ICollection<IModule> components)
        {
            if (components == null || !components.Any()) return;

            foreach (var component in components)
            {
                if (component.Rules == null || !component.Rules.Any()) continue;

                foreach (var moduleRule in component.Rules)
                {
                    if (!moduleRule.Properties.ContainsKey(JobsKeys.ComponentKey))
                        moduleRule.Properties.Add(JobsKeys.ComponentKey, component);
                    if (!moduleRule.Properties.ContainsKey(JobsKeys.HubKey))
                        moduleRule.Properties.Add(JobsKeys.HubKey, _hubService);
                    if (!moduleRule.Properties.ContainsKey(JobsKeys.RuleKey))
                        moduleRule.Properties.Add(JobsKeys.RuleKey, moduleRule);
                    await moduleRule.Execute();
                }
            }
        }

        public async Task StopJobs()
        {
            var scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            if (scheduler != null && scheduler.IsStarted)
            {
                await scheduler.Clear();
                await scheduler.Shutdown();
            }
        }

    }
}
