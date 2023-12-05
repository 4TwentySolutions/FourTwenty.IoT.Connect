using System;

namespace FourTwenty.IoT.Connect.Data
{
    public class BaseData : IBaseData
    {
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public virtual string Value { get; protected set; }
    }

    public interface IBaseData
    {
        DateTime DateCreated { get; set; }
        string Value { get; }
    }


}
