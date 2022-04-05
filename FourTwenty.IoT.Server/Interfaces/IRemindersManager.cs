using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FourTwenty.IoT.Server.ViewModels;

namespace FourTwenty.IoT.Server.Interfaces
{
    public interface IRemindersManager
    {
        Task<List<ReminderVm>> GetReminders(DateTime? dateFrom = null);
        Task SaveReminder(ReminderVm reminder);
        Task DeleteReminder(ReminderVm reminder);
    }
}
