using Quartz.Spi;
using System;
using Quartz;
using Microsoft.Extensions.DependencyInjection;

namespace FourTwenty.IoT.Server.Jobs
{
    public class BoxJobFactory : IJobFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public BoxJobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return _serviceProvider.GetRequiredService(bundle.JobDetail.JobType) as IJob;
        }

        public void ReturnJob(IJob job)
        {
            var disposable = job as IDisposable;
            disposable?.Dispose();
        }
    }
}
