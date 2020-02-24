using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FourTwenty.IoT.Connect.Interfaces;

namespace FourTwenty.IoT.Server.Interfaces
{
    public interface IMessagesService
    {
        Task SendMessage(IModule module, params object[] value);
    }
}
