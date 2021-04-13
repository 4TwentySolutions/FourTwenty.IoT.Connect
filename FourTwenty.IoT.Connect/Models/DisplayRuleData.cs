using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Connect.Interfaces;

namespace FourTwenty.IoT.Connect.Models
{
	public class DisplayRuleData : IRuleData
	{
		public int DisplayOrder { get; set; }
		public DisplayType DisplayType { get; set; }
		public string DisplayOptionParams { get; set; }
	}
}
