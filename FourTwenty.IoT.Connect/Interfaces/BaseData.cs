using System;
using System.Collections.Generic;
using FourTwenty.IoT.Connect.Constants;

namespace FourTwenty.IoT.Connect.Interfaces
{
    public abstract class BaseData
    {
        public DateTime DateCreated { get; set; }
        public string Value { get; set; }

        protected BaseData()
        {
            DateCreated = DateTime.Now;
        }
    }

    public class DhtData : TempData
    {
        public DhtData(double temp, double humidity) : base(temp)
        {
            Temperature = temp;
            Humidity = humidity;
            Value = ToString();
        }

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
    }

    public class RangeFinderData : BaseData
    {
        public RangeFinderData(double distance)
        {
            Distance = distance;
            Value = distance.ToString();
        }

        public double Distance { get; set; }
    }

    public class RelayData : BaseData
    {
        public RelayData(int pin, RelayState state)
        {
            Pin = pin;
            State = state;
            Value = $"#{pin}={state.ToString()}";
        }

        public int Pin { get; set; }

        public RelayState State { get; set; }

        public override string ToString()
        {
            return $"{nameof(Pin)}: {State}";
        }
    }

    public class TempData : BaseData
    {
        public TempData(double temperature)
        {
            Temperature = temperature;
            Value = temperature.ToString();
        }

        /// <summary>
        /// Temperature in celsius
        /// </summary>
        public double Temperature { get; set; }
    }

    public class CameraData : BaseData
    {
        /// <summary>
        /// Path to the file 
        /// </summary>
        public CameraData(string path)
        {
            Value = path;
        }
    }

    public class SoilMoistureData : BaseData
    {
        public double Moisture { get; set; }
    }

    public class PinValueData : BaseData
    {
        public int PinValue { get; set; }

        public PinValueData(int pinValue)
        {
            PinValue = pinValue;
            Value = pinValue.ToString();
        }
    }


    /// <summary>
    /// Model for reading baseData from MCP3008 by python
    /// </summary>
    public class ADCData
    {
        public int Channel { get; set; }
        public double Value { get; set; }
        public double Voltage { get; set; }
    }
}
