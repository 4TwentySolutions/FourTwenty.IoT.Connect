using FourTwenty.IoT.Connect.Constants;

namespace FourTwenty.IoT.Connect.Entities
{
    public class ModuleRule : BaseEntity<int>
    {
        public JobType Job { get; set; }
        public RuleType RuleType { get; set; }
        /// <summary>
        /// Could be complex type (json , separated string's etc)
        /// </summary>
        public string RuleContent { get; set; }
        public int GrowBoxModuleId { get; set; }
        public virtual GrowBoxModule GrowBoxModule { get; set; }
    }
}
