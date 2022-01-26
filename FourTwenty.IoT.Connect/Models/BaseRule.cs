using System.Collections.Generic;
using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Connect.Interfaces;

namespace FourTwenty.IoT.Connect.Models
{
    public class BaseRule : IRule
    {
        public int Id { get; set; }
        public string RuleName { get; set; }
        public RuleType RuleType { get; protected set; }
        public bool IsEnabled { get; set; }
        public int? Pin { get; set; }
        public int SortOrder { get; set; }
        public long? Delay { get; set; }
    }
}
