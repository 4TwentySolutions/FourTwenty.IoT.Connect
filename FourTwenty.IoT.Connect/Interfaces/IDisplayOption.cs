using System;
using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Connect.Models;

namespace FourTwenty.IoT.Connect.Interfaces
{
	public interface IDisplayOption
	{
		public DisplayType DisplayType { get; }
		public IParams Options { get; set; }
		public int DisplayOrder { get; set; }
		string Execute(string value);
		IData Execute(ModuleType type, IData data);
	}

	public interface IParams { }

	public interface IRuleData { }

	public interface IData
	{
		string Value { get; set; }
	}
}
