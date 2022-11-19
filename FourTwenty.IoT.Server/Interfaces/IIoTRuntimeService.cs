using System.Collections.Generic;
using System.Device.Gpio;
using System.Threading.Tasks;
using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Connect.Interfaces.Services;
using FourTwenty.IoT.Server.Components;
using FourTwenty.IoT.Server.Models.Services;
using FourTwenty.IoT.Server.ViewModels;
using IComponent = FourTwenty.IoT.Connect.Interfaces.IComponent;

namespace FourTwenty.IoT.Server.Interfaces
{
    public interface IIoTRuntimeService : IInitializeService<IoTRuntimeServiceModel>
    {
        IList<IComponent> GetComponents();
        GpioController Gpio { get; }
        IComponent GetModule(int id);
        IList<ModuleVm> GetModules();
        //Task ControlModuleJobs(int moduleId, WorkState WorkState);
        Task<IoTComponent> ConfigureModule(ModuleVm module);
        IComponent GetModuleByType(ComponentType componentType);
    }
}
