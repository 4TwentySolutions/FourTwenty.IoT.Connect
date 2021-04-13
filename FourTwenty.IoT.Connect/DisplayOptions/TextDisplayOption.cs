using System;
using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Connect.Interfaces;

namespace FourTwenty.IoT.Connect.DisplayOptions
{
	public class TextDisplayOption : IDisplayOption
	{
		public DisplayType DisplayType => DisplayType.Text;
		public IParams Options { get; set; }
		public int DisplayOrder { get; set; }

		public string Execute(string value)
		{
			if (Options is TextParams opt)
			{
				return !string.IsNullOrEmpty(opt.IconWrapper) ? string.Format(opt.IconWrapper, value) : value;
			}

			return value;
		}

		public IData Execute(ModuleType type, IData data)
		{
			if (Options is TextParams opt && data != null)
			{
				switch (type)
				{
					case ModuleType.RangeFinder when data is RangeFinderData rfData:
						data.Value = Execute(string.IsNullOrEmpty(data.Value) ?
							rfData.Distance.ToString() :
							data.Value);
						break;
					case ModuleType.HumidityAndTemperature when data is DhtData dhtData:
						data.Value = !string.IsNullOrEmpty(data.Value) ?
							Execute(data.Value) :
							string.Format(opt.IconWrapper, dhtData.Temperature, dhtData.Humidity);
						break;
				}
			}

			return data;
		}
	}

	public class TextParams : IParams
	{
		public string IconWrapper { get; set; }
	}
}
