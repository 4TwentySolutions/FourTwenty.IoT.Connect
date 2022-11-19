using FourTwenty.IoT.Connect.Entities;
using FourTwenty.IoT.Server.ViewModels;
using System.Collections.Generic;

namespace FourTwenty.IoT.Server.Models.Services
{
    public class IoTRuntimeServiceModel
    {
        public GrowBoxViewModel GrowBoxViewModel { get; set; }
        public Dictionary<int, ModuleHistoryItemVm> ModulesHistory { get; set; }
    }
}
