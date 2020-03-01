using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Connect.Interfaces;
using FourTwenty.IoT.Server.Jobs;
using Quartz;
using Quartz.Impl;

namespace FourTwenty.IoT.Server.Rules
{
    public class CronRule : IPeriodRule
    {
        private readonly IScheduler _scheduler;

        public TimeSpan Period { get; set; }
        public JobType JobType { get; set; }
        public string CronExpression { get; set; }
        public CronRule(JobType jobType, string cronExpression, IScheduler scheduler)
        {
            JobType = jobType;
            CronExpression = cronExpression;
            _scheduler = scheduler;
        }
        public IDictionary<string, object> Properties { get; } = new Dictionary<string, object>();

        public async Task Execute()
        {
            if(_scheduler == null)
                return;

            if (_scheduler.InStandbyMode)
                await _scheduler.Start();
            Type jobType = null;

            switch (JobType)
            {
                case JobType.Toggle:
                    jobType = typeof(ToggleJob);
                    break;
                case JobType.Read:
                    jobType = typeof(ReadJob);
                    break;
                case JobType.On:
                    jobType = typeof(OnJob);
                    break;
                case JobType.Off:
                    jobType = typeof(OffJob);
                    break;
                case JobType.Period:
                    jobType = typeof(PeriodJob);
                    break;
            }

            if (jobType == null)
                return;


            JobDataMap jobData = new JobDataMap(Properties);

            var job = JobBuilder.Create(jobType).UsingJobData(jobData).Build();
            var trigger = TriggerBuilder.Create()
                .WithIdentity($"{Guid.NewGuid()}_{JobType}_Job", JobType.ToString())
                .WithCronSchedule(CronExpression)
                .StartNow()
                .WithPriority(1)
                .Build();

            await _scheduler.ScheduleJob(job, trigger);
        }
    }
}
