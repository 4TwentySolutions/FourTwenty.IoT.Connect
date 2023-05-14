using System.Collections.Generic;
using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Connect.Rules;
using GrowIoT.Rules;

namespace FourTwenty.IoT.Connect.Interfaces
{
	public interface IComponent : IPinComponent
	{
		int Id { get; set; }
		IReadOnlyCollection<CronRule> Rules { get; set; }
		IReadOnlyCollection<DisplayRule> DisplayOptions { get; set; }
		string Name { get; set; }
		WorkState RulesWorkState { get; set; }
        ComponentType ComponentType { get; }
	}
}
