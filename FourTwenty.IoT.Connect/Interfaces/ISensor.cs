using System;
using System.Threading.Tasks;

namespace FourTwenty.IoT.Connect.Interfaces
{
    public interface ISensor
    {
        event EventHandler<SensorEventArgs> DataReceived;
        ValueTask<object> GetData();
    }

    public class SensorEventArgs : EventArgs
    {
        public object Data { get; }

        public SensorEventArgs(object data)
        {
            Data = data;
        }
    }
}
