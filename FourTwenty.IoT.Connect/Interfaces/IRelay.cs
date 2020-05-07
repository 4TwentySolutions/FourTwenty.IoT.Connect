using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FourTwenty.IoT.Connect.Constants;

namespace FourTwenty.IoT.Connect.Interfaces
{
    public interface IRelay : IPinComponent
    {
        IDictionary<int, RelayState> States { get; }

        event EventHandler<RelayEventArgs> StateChanged;
    }

    public class RelayEventArgs : EventArgs
    {
        public RelayData Data { get; }

        public RelayEventArgs(RelayData data)
        {
            Data = data;
        }
    }

    public class RelayData
    {
        public RelayData() { }

        public RelayData(int pin, RelayState state)
        {
            Pin = pin;
            State = state;
        }

        public int Pin { get; set; }
        public RelayState State { get; set; }

        /// <summary>
        /// Return DHT sensor values:
        ///     - Temperature (celsius)
        ///     - Humidity
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{nameof(Pin)}: {State}";
        }
    }
}
