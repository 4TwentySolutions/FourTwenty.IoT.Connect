using FourTwenty.IoT.Connect.Constants;

namespace FourTwenty.IoT.Connect.Interfaces.Rules
{
    public interface IRule
    {
        int Id { get; set; }
        public RuleType RuleType { get; }
        bool IsEnabled { get; set; }
    }
}
