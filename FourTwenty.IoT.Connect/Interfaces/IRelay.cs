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
        public RelayState State { get; }
        public int Pin { get; }

        public RelayEventArgs(RelayState state, int pin)
        {
            State = state;
            Pin = pin;
        }
    }
}
