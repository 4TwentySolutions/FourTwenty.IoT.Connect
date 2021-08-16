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
	
	public class DhtData : IData
	{
		public DhtData(double temp, double humidity)
		{
			Temperature = temp;
			Humidity = humidity;
		}

		/// <summary>
		/// Temperature in celsius
		/// </summary>
		public double Temperature { get; set; }
		public double Humidity { get; set; }

		/// <summary>
		/// Return DHT sensor values:
		///     - Temperature (celsius)
		///     - Humidity
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return $"{nameof(Temperature)}: {Temperature}\n{nameof(Humidity)}: {Humidity}";
		}

		public string Value { get; set; }
	}

	public class RangeFinderData : IData
	{
		public RangeFinderData(double distance)
		{
			Distance = distance;
		}

		public double Distance { get; set; }
		public string Value { get; set; }
	}

	public class RelayData : IData
	{
		public RelayData(int pin, RelayState state)
		{
			Pin = pin;
			State = state;
            Value = $"#{pin}={state.ToString()}";
        }

		public int Pin { get; set; }

		public RelayState State { get; set; }

		public string Value { get; set; }

		public override string ToString()
		{
			return $"{nameof(Pin)}: {State}";
		}
	}
}
