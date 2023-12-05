using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Connect.Interfaces.Rules;

namespace FourTwenty.IoT.Connect.Models
{
	public class DisplayRuleData : IRuleData
	{
		public int DisplayOrder { get; set; }
		public DisplayType DisplayType { get; set; }
		public string DisplayOptionParams { get; set; }
        public bool IsEnabled { get; set; }
        public int? Pin { get; set; }

        public override string ToString()
        {
            return $"{DisplayType}; DisplayOrder={DisplayOrder}; Pin={(Pin.HasValue ? Pin.GetValueOrDefault() : "")};";
        }
    }
}
