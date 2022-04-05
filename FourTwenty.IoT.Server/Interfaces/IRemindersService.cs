using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FourTwenty.IoT.Server.ViewModels;

namespace FourTwenty.IoT.Server.Interfaces
{
    public interface IRemindersService
    {
        Task StartJobs();
        Task RefreshReminderJob(ReminderVm reminder);
    }
}
