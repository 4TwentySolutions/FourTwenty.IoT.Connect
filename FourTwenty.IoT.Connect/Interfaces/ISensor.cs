using System;
using System.Threading.Tasks;
using FourTwenty.IoT.Connect.Common;

namespace FourTwenty.IoT.Connect.Interfaces
{
    public interface ISensor<T> : IPinComponent
    {
        ValueTask<T> GetData();
        event EventHandler<SensorDataReceivedEventArgs<T>> DataReceived;
    }

}
