using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Connect.Models;

namespace FourTwenty.IoT.Connect.Interfaces
{
    public interface ISensor
    {
        event EventHandler<ModuleResponseEventArgs> DataReceived;
        ValueTask<ModuleResponse> GetData(Dictionary<string,object> additionalParams);
        SensorReadType ReadType { get; }
    }
}
