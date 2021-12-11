using System;
using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Connect.Interfaces;

namespace FourTwenty.IoT.Connect.DisplayOptions
{
	public class PercentDisplayOption : IDisplayOption
	{
        public bool IsEnabled { get; set; }
        public DisplayType DisplayType => DisplayType.Percent;
		public IParams Options { get; set; }
		public int DisplayOrder { get; set; }
        public int? Pin { get; set; }

        public string Execute(string val)
		{
            if (!double.TryParse(val, out var value) 
                || Options is not PercentParams opt 
                || opt.Max == -1 
                || opt.Min == -1) 
                return val;
            var v = (opt.Max - value) / (opt.Max / opt.Min);
            v = v > 0 ? 100 - v : 100 + v;

            val = Math.Round(v, 2) + "%";

            return val;
		}

		public IData Execute(ComponentType type, IData data)
		{
			if (Options is PercentParams opt && data != null)
			{
				switch (type)
				{
					case ComponentType.RangeFinder when data is RangeFinderData rfData:
						data.Value = Execute(rfData.Distance.ToString());
						break;
					case ComponentType.HumidityAndTemperature when data is DhtData dhtData:
						data.Value = $"{dhtData.Temperature}\u00B0C,{dhtData.Humidity}%";
						break;
				}
			}

			return data;
		}
	}

	public class PercentParams : IParams
	{
		public double Min { get; set; }
		public double Max { get; set; }
	}
}
