using System;
using System.Collections.Generic;
using System.Text;
using FourTwenty.IoT.Connect.Constants;

namespace FourTwenty.IoT.Connect.Interfaces
{
    public interface IAction
    {
        ModuleType ModuleType { get; }
        ActionType ActionType { get; }

        string Execute();
    }
}
