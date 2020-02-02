using System;

namespace FourTwenty.IoT.Connect.Common
{
    public class SensorDataReceivedEventArgs<T> : EventArgs
    {
        public T Data { get; }

        public SensorDataReceivedEventArgs(T data)
        {
            Data = data;
        }
    }
}
