using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FourTwenty.IoT.Connect.Constants;

namespace FourTwenty.IoT.Connect.Interfaces
{
    public interface IAction
    {
        ComponentType SensorType { get; }
        ActionType ActionType { get; }
        IRuleData Data { get; }
        Task Execute();
    }
}
