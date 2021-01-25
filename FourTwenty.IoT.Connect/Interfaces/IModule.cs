using System.Collections.Generic;
using FourTwenty.IoT.Connect.Constants;

namespace FourTwenty.IoT.Connect.Interfaces
{
	public interface IModule : IPinComponent
	{
		int Id { get; set; }
		IReadOnlyCollection<IRule> Rules { get; set; }
		string Name { get; set; }
		WorkState RulesWorkState { get; set; }
	}
}
