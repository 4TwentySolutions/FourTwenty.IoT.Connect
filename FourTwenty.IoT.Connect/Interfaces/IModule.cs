using System;
using System.Collections.Generic;

namespace FourTwenty.IoT.Connect.Interfaces
{
    public interface IModule : IPinComponent
    {
        int Id { get; set; }
        IReadOnlyCollection<IRule> Rules { get; set; }
        string Name { get; set; }
    }
}
