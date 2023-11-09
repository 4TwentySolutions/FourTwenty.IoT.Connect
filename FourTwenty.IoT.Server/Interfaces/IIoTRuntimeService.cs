using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Threading.Tasks;
using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Connect.Entities;
using FourTwenty.IoT.Connect.Interfaces;
using FourTwenty.IoT.Connect.Models;
using FourTwenty.IoT.Server.Components;
using FourTwenty.IoT.Server.ViewModels;
using IComponent = FourTwenty.IoT.Connect.Interfaces.IComponent;

namespace FourTwenty.IoT.Server.Interfaces
{
    public interface IIoTRuntimeService : IInitializable
    {
        event EventHandler<ModuleResponseEventArgs> ComponentDataReceived;

        IList<IComponent> GetComponents();
        IComponent GetComponent(int id);
        GpioController Gpio { get; }
        Task<bool> ConfigureModule(GrowBoxModule module);
        Task<bool> ConfigureModule(int componentId);
        IComponent GetModuleByType(ComponentType componentType);
    }
}
