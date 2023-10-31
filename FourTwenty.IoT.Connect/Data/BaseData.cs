using System;

namespace FourTwenty.IoT.Connect.Data
{
    public abstract class BaseData
    {
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public string Value { get; set; }
    }
}
