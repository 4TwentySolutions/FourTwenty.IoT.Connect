using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Connect.Data;
using FourTwenty.IoT.Connect.Interfaces.Rules;
using FourTwenty.IoT.Connect.Models;

namespace FourTwenty.IoT.Connect.Rules
{
    public class DisplayRule : BaseRule, IDisplayRule
    {
        public DisplayType DisplayType { get; set; }
        public IParams Options { get; set; }

        public DisplayRule()
        {
            RuleType = RuleType.Display;
        }

        public virtual string Execute(string value)
        {
            return string.Empty;
        }

        public virtual BaseData Execute(ComponentType type, BaseData data)
        {
            return data;
        }
    }
}
