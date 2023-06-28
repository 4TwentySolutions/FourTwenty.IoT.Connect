using System;

namespace FourTwenty.IoT.Connect.Data
{
    public abstract class BaseData
    {
        public DateTime DateCreated { get; set; }
        public string Value { get; set; }

        protected BaseData()
        {
            DateCreated = DateTime.Now;
        }
    }
}
