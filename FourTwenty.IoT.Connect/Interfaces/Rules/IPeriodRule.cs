using System;

namespace FourTwenty.IoT.Connect.Interfaces.Rules
{
    public interface IPeriodRule
    {
        TimeSpan? Period { get; }
    }
}
