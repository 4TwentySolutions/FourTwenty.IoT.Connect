using System.Threading.Tasks;
using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Server.Interfaces;
using FourTwenty.IoT.Server.ViewModels;
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
            ReminderVm reminder = null;

            if (context.JobDetail.JobDataMap.TryGetValue(JobsKeys.ReminderKey, out var rawReminderObj))
                reminder = rawReminderObj as ReminderVm;

            if (reminder != null)
            {
                if (!reminder.IsExecuted)
                {
                    if (reminder.Source == CommunicationСhannel.Telegram)
                    {
                       var sendResult = await _telegramBotService.SendMessage(reminder.Description);

                       if (sendResult)
                       {
                           reminder.IsExecuted = true;
                           await _remindersManager.SaveReminder(reminder);
                       }

                    }
                }
            }
        }
    }
}
