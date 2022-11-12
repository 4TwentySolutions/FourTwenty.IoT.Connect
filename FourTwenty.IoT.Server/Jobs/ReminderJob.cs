using System;
using System.Device.Gpio;
using System.Threading.Tasks;
using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Connect.Interfaces;
using FourTwenty.IoT.Server.Components;
using FourTwenty.IoT.Server.Interfaces;
using FourTwenty.IoT.Server.ViewModels;
using Quartz;

namespace FourTwenty.IoT.Server.Jobs
{
    public class ReminderJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            ITelegramBotService telegramBotService = null;
            IRemindersManager remindersManager = null;
            ReminderVm reminder = null;

            if (context.JobDetail.JobDataMap.TryGetValue(JobsKeys.TelegramBotServiceKey, out var rawObj))
                telegramBotService = rawObj as ITelegramBotService;

            if (context.JobDetail.JobDataMap.TryGetValue(JobsKeys.ReminderManagerKey, out var rawReminderManagerObj))
                remindersManager = rawReminderManagerObj as IRemindersManager;

            if (context.JobDetail.JobDataMap.TryGetValue(JobsKeys.ReminderKey, out var rawReminderObj))
                reminder = rawReminderObj as ReminderVm;

            if (reminder != null && telegramBotService != null && remindersManager != null)
            {
                if (!reminder.IsExecuted)
                {
                    if (reminder.Source == CommunicationСhannel.Telegram)
                    {
                       var sendResult = await telegramBotService.SendMessage(reminder.Description);

                       if (sendResult)
                       {
                           reminder.IsExecuted = true;
                           await remindersManager.SaveReminder(reminder);
                       }

                    }
                }
            }
        }
    }
}
