using System;
using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Connect.Models;

namespace FourTwenty.IoT.Connect.Interfaces
{
	public interface IDisplayOption
	{
        public bool IsEnabled { get; set; }
        public DisplayType DisplayType { get; }
		public IParams Options { get; set; }
		public int DisplayOrder { get; set; }
        public int? Pin { get; set; }
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
