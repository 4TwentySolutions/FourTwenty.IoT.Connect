using System.Collections.Generic;

namespace FourTwenty.IoT.Connect.Interfaces
{
    public interface IModule : IPinComponent
    {
        IReadOnlyCollection<IRule> Rules { get; set; }
    }
}
