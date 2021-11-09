using System.Collections.Generic;
using FourTwenty.IoT.Connect.Constants;

namespace FourTwenty.IoT.Connect.Interfaces
{
	public interface IComponent : IPinComponent
	{
		int Id { get; set; }
		IReadOnlyCollection<IRule> Rules { get; set; }
		IReadOnlyCollection<IDisplayOption> DisplayOptions { get; set; }
		string Name { get; set; }
		WorkState RulesWorkState { get; set; }
        ComponentType ComponentType { get; }
	}
}
