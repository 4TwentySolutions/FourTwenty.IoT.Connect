using System;
using System.Collections.Generic;
using System.Text;
using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Connect.Entities;

namespace FourTwenty.IoT.Connect.Dto
{
    public class ModuleRuleDto
    {
        public string CronExpression { get; set; }
        public JobType Type { get; set; }

        /// <summary>
        /// Period in seconds
        /// </summary>
        public int Period { get; set; }
    }
}
