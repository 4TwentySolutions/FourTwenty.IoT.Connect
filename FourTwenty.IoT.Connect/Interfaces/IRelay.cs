using System.Collections.Generic;
using System.Threading.Tasks;
using FourTwenty.IoT.Connect.Constants;

namespace FourTwenty.IoT.Connect.Interfaces
{
    public interface IRelay : IPinComponent
    {
        IDictionary<int, RelayState> States { get; }
        ValueTask Open(int pin);
        ValueTask Close(int pin);

    }
}
