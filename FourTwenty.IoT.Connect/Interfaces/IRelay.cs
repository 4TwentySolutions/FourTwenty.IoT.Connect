using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Connect.Models;

namespace FourTwenty.IoT.Connect.Interfaces
{
    public interface IRelay : IPinComponent
    {
        IDictionary<int, RelayState> States { get; }

        event EventHandler<ModuleResponseEventArgs> StateChanged;
    }

    public class RelayData
    {
	    public RelayData(int pin, RelayState state)
        {
            Pin = pin;
            State = state;
        }

        public int Pin { get; set; }

        public RelayState State { get; set; }
        
        public override string ToString()
        {
            return $"{nameof(Pin)}: {State}";
        }
    }
}
