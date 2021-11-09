using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Connect.Interfaces;

namespace FourTwenty.IoT.Connect.Models
{
    public class ActionRuleData : IRuleData
    {
        public ActionType ActionType { get; set; }
        public int ModuleId { get; set; }
        public JobType JobType { get; set; }
    }
}
