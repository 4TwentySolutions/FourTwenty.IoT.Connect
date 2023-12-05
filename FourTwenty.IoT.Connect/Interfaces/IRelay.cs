using System;
using System.Collections.Generic;
using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Connect.Models;

namespace FourTwenty.IoT.Connect.Interfaces
{
	public interface IRelay : IPinComponent
	{
		IDictionary<int, RelayState> States { get; }

		event EventHandler<ModuleResponseEventArgs> StateChanged;
	}
}
