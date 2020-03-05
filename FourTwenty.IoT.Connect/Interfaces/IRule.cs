using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FourTwenty.IoT.Connect.Interfaces
{
    public interface IRule
    {
        int? Pin { get; set; }
        IDictionary<string, object> Properties { get; }
        Task Execute();
    }

    public interface IPeriodRule : IRule
    {
        TimeSpan Period { get; }
    }
}
