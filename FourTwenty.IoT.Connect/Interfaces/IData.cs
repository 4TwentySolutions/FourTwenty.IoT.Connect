using System.Collections.Generic;
using FourTwenty.IoT.Connect.Constants;

namespace FourTwenty.IoT.Connect.Interfaces
{
    public interface IData
    {
        string Value { get; set; }
    }

    public class DhtData : IData
    {
        public DhtData(double temp, double humidity)
        {
            Temperature = temp;
            Humidity = humidity;
            Value = ToString();
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
        public sealed override string ToString()
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
            Value = distance.ToString();
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

    public class TempData : IData
    {
        public TempData(double temperature)
        {
            Temperature = temperature;
            Value = temperature.ToString();
        }

        public double Temperature { get; set; }
        public string Value { get; set; }
    }

    public class CameraData : IData
    {
        public CameraData(string path)
        {
            Value = path;
        }
        
        /// <summary>
        /// Path to the file 
        /// </summary>
        public string Value { get; set; }
    }

    public class SoilMoistureData : IData
    {
        public string Value { get; set; }
    }


    /// <summary>
    /// Model for reading data from MCP3008 by python
    /// </summary>
    public class ADCData
    {
        public int Channel { get; set; }
        public double Value { get; set; }
        public double Voltage { get; set; }
    }
}
