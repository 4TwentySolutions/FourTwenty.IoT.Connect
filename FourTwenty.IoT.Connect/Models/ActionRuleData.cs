using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Connect.Interfaces.Rules;

namespace FourTwenty.IoT.Connect.Models
{
    public class ActionRuleData : IRuleData
    {
        public ActionType ActionType { get; set; }
        public int ModuleId { get; set; }
        public ActionJobType ActionJobType { get; set; }
        public int? Pin { get; set; }
        public int Delay { get; set; }
        public int CompareValue { get; set; }
        public ComparisonDirection ComparisonDirection { get; set; }
        public ComparisonItem ComparisonItem { get; set; }
        public string Message { get; set; }
    }
}
