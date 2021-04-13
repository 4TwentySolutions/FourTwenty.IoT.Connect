﻿using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Connect.Interfaces;

namespace FourTwenty.IoT.Connect.Models
{
    public class CronRuleData : IRuleData
    {
        public string CronExpression { get; set; }
        public long? Delay { get; set; }
        public JobType Job { get; set; }
    }
}
