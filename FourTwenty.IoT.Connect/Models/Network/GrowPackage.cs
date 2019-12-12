using System;
using FourTwenty.IoT.Connect.Constants;

namespace FourTwenty.IoT.Connect.Models
{
    public class GrowPackage<T> : GrowPackage
    {
        public string Host { get; set; }
        public T Data { get; set; }

        public GrowPackage(T data)
        {
            Data = data;
        }
    }

    public class GrowPackage
    {
        public string Host { get; set; }

        public GrowPackage()
        {
        }
    }
}
