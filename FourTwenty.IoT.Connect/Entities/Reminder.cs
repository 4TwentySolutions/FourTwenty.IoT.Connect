using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using FourTwenty.Core.Data.Models;
using FourTwenty.IoT.Connect.Constants;

namespace FourTwenty.IoT.Connect.Entities
{
    public class Reminder : BaseEntity<int>
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ExecuteTime { get; set; }
        public СommunicationСhannel Source { get; set; }
        public bool IsExecuted { get; set; }
    }
}
