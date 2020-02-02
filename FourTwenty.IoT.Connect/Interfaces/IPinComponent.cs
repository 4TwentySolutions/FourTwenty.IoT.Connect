using System.Collections.Generic;

namespace FourTwenty.IoT.Connect.Interfaces
{
    public interface IPinComponent
    {
        IReadOnlyCollection<int> Pins { get; } 
    }
}
