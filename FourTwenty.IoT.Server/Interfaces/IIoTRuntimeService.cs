using System.Collections.Generic;
using System.Device.Gpio;
using System.Threading.Tasks;
using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Connect.Interfaces;
using FourTwenty.IoT.Connect.Interfaces.Services;
using FourTwenty.IoT.Server.Components;
using FourTwenty.IoT.Server.ViewModels;

namespace FourTwenty.IoT.Server.Interfaces
{
    public interface IIoTRuntimeService : IInitializeService<GrowBoxViewModel>
    {
        IList<IComponent> GetModules();
        GpioController Gpio { get; }
        IComponent GetModule(int id);
        Task ControlModuleJobs(int moduleId, WorkState WorkState);
        Task<IoTComponent> ConfigureModule(ModuleVm module);
        IComponent GetModuleByType(ComponentType componentType);
    }
}
