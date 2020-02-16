using FourTwenty.IoT.Connect.Constants;

namespace FourTwenty.IoT.Connect.Models
{
    public class CronRuleData
    {
        public string CronExpression { get; set; }
        public long? Delay { get; set; }
        public JobType Job { get; set; }
    }
}
