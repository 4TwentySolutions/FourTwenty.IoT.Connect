using System;

namespace FourTwenty.IoT.Connect.Models
{
    public class GrowPackage<T> : GrowPackage
    {
        public T Data { get; set; }

        public GrowPackage(T data)
        {
            Data = data;
        }

        public GrowPackage(Uri host, T data) : base(host)
        {
            Data = data;
        }

        public GrowPackage(string host, T data) : base(host)
        {
            Data = data;
        }
    }

    public class GrowPackage
    {
        public Uri Host { get; set; }

        public GrowPackage(string host)
        {
            Host = new Uri(host);
        }

        public GrowPackage(Uri host)
        {
            Host = host;
        }

        public GrowPackage() { }
    }
}
