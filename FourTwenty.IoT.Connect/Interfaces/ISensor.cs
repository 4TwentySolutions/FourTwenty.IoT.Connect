using System;
using System.Threading.Tasks;

namespace FourTwenty.IoT.Connect.Interfaces
{
    public interface ISensor
    {
        Guid Id { get; set; }
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
