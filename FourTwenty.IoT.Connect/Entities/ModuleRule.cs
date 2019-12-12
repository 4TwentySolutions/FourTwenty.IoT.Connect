using FourTwenty.IoT.Connect.Constants;

namespace FourTwenty.IoT.Connect.Entities
{
    public class ModuleRule : BaseEntity<int>
    {
        public string CronExpression { get; set; }
        public JobType Type { get; set; }
        public int GrowBoxModuleId { get; set; }

        /// <summary>
        /// Period in seconds
        /// </summary>
        public int Period { get; set; }
        public virtual GrowBoxModule GrowBoxModule { get; set; }
    }
}
