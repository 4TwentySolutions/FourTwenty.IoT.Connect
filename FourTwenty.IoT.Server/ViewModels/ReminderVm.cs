using System;
using System.ComponentModel.DataAnnotations;
using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Connect.Entities;

namespace FourTwenty.IoT.Server.ViewModels
{
    public class ReminderVm : EntityViewModel<Reminder>
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ExecuteTime { get; set; }
        public СommunicationСhannel Source { get; set; }
        public bool IsExecuted { get; set; }
    }
}
