using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Connect.Interfaces;
using FourTwenty.IoT.Connect.Interfaces.Rules;
using FourTwenty.IoT.Connect.Rules;

namespace FourTwenty.IoT.Server.DisplayOptions
{
	public class TextDisplayRule : DisplayRule
	{
        public override string Execute(string value)
		{
			if (Options is TextParams opt)
			{
				return !string.IsNullOrEmpty(opt.IconWrapper) ? string.Format(opt.IconWrapper, value) : value;
			}

			return value;
		}

		public override BaseData Execute(ComponentType type, BaseData data)
		{
			if (Options is TextParams opt && data != null)
			{
				switch (type)
				{
					case ComponentType.RangeFinder when data is RangeFinderData rfData:
						data.Value = Execute(string.IsNullOrEmpty(data.Value) ?
							rfData.Distance.ToString() :
							data.Value);
						break;
					case ComponentType.HumidityAndTemperature when data is DhtData dhtData:
						data.Value = !string.IsNullOrEmpty(data.Value) ?
							Execute(data.Value) :
							string.Format(opt.IconWrapper, dhtData.Temperature, dhtData.Humidity);
						break;
				}
			}

            if (Options is RelayTextParams relayOpt && data is RelayData relayData)
            {
                if (Pin.HasValue)
                {
                    if (Pin.GetValueOrDefault() == relayData.Pin)
                    {
                        data.Value = relayData.State == RelayState.Opened
                            ? relayOpt.OpenedStateIconWrapper
                            : relayOpt.ClosedStateIconWrapper;
                    }
                }
            }

			return data;
		}
	}

	public class TextParams : IParams
	{
		public string IconWrapper { get; set; }
	}

    public class RelayTextParams : IParams
    {
        public string OpenedStateIconWrapper { get; set; }
        public string ClosedStateIconWrapper { get; set; }
    }
}
