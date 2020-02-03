namespace FourTwenty.IoT.Connect.Models
{
    public class CronRule
    {
        public string CronExpression { get; set; }
        public long? Delay { get; set; }
    }
}
