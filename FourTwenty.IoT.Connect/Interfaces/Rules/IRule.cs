using System.Collections.Generic;
using FourTwenty.IoT.Connect.Constants;

namespace FourTwenty.IoT.Connect.Interfaces
{
    public interface IRule
    {
        int Id { get; set; }
        public string RuleName { get; set; }
        public RuleType RuleType { get; }
        bool IsEnabled { get; set; }
	    int? Pin { get; set; }
        public int SortOrder { get; set; }
        public long? Delay { get; set; }
    }
}
