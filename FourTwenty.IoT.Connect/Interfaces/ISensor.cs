﻿using System;
using System.Threading.Tasks;
using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Connect.Models;

namespace FourTwenty.IoT.Connect.Interfaces
{
    public interface ISensor
    {
        event EventHandler<ModuleResponseEventArgs> DataReceived;
        ValueTask<ModuleResponse<BaseData>> GetData();
        SensorReadType ReadType { get; }
    }
}
