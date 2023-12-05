using System.Threading.Tasks;
using FourTwenty.IoT.Server.Interfaces;
using FourTwenty.IoT.Server.Publishers;
using GrowIoT.MessageQueue.Interfaces;
using Quartz;

namespace FourTwenty.IoT.Server.Jobs
{
    public class ReminderJob : IJob
    {
        private readonly ITelegramBotService _telegramBotService;
        private readonly IRemindersManager _remindersManager;

        public ReminderJob(ITelegramBotService telegramBotService, IRemindersManager remindersManager)
        {
            _telegramBotService = telegramBotService;
            _remindersManager = remindersManager;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            int? reminderId = null;
            IBasicPublisher<int> publisher = null;

            if (context.JobDetail.JobDataMap.TryGetValue(JobsKeys.ReminderIdKey, out var rawReminderIdObj))
                reminderId = (int)rawReminderIdObj;

            if (context.JobDetail.JobDataMap.TryGetValue(JobsKeys.ReminderPublishKey, out var rawPublisher))
                publisher = rawPublisher as ReminderPublisher;

            if(reminderId.HasValue && publisher != null)
                publisher.Publish(reminderId.GetValueOrDefault());
        }
    }
}
