using System.Collections.Generic;
using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Connect.Entities;
using FourTwenty.IoT.Connect.Interfaces;
using FourTwenty.IoT.Connect.Interfaces.Rules;
using FourTwenty.IoT.Connect.Models;
using FourTwenty.IoT.Connect.Rules;

namespace FourTwenty.IoT.Server.ViewModels
{
	public class ModuleRuleVm : EntityVm<ModuleRule>
	{
		public int Id { get; set; }
        public string RuleName { get; set; }
		public JobType Job { get; set; }
		public RuleType RuleType { get; set; }
		public string RuleContent { get; set; }
		public int GrowBoxModuleId { get; set; }
		public List<int> Pins { get; set; }
		public int? Pin { get; set; }
		public bool IsEnabled { get; set; }
		public DisplayRule DisplayRule { get; set; }
		public int SortOrder { get; set; }
		public IRuleData RuleData { get;set; }

        public string TextName => $"{RuleType} ({RuleData.ToString()})";

    }
}
