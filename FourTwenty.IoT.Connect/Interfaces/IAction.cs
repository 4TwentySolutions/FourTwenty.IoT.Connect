using System;
using System.Collections.Generic;
using System.Text;
using FourTwenty.IoT.Connect.Constants;

namespace FourTwenty.IoT.Connect.Interfaces
{
    public interface IAction
    {
        ComponentType SensorType { get; }
        ActionType ActionType { get; }

        string Execute();
    }
}
