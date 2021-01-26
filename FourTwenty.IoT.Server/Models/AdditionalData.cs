namespace FourTwenty.IoT.Server.Models
{
	public class AdditionalData
	{
		public bool? IsPercents { get; set; }
		public string Min { get; set; }
		public string Max { get; set; }

		private double? _minValue;
		public double MinValue
		{
			get
			{
				if (_minValue.HasValue)
				{
					return _minValue.GetValueOrDefault();
				}

				if (double.TryParse(Min, out var res))
				{
					_minValue = res;
					return _minValue.GetValueOrDefault();
				}

				return -1;
			}
		}

		private double? _maxValue;
		public double MaxValue
		{
			get
			{
				if (_maxValue.HasValue)
				{
					return _maxValue.GetValueOrDefault();
				}

				if (double.TryParse(Max, out var res))
				{
					_maxValue = res;
					return _maxValue.GetValueOrDefault();
				}

				return -1;
			}
		}
	}
}
