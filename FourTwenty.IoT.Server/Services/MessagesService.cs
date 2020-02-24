using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FourTwenty.IoT.Connect.Interfaces;
using FourTwenty.IoT.Server.Interfaces;

namespace FourTwenty.IoT.Server.Services
{
    public class MessagesService : IMessagesService
    {
        private readonly IHubService _hubService;

        public MessagesService()
        {

        }

        public MessagesService(IHubService hubService)
        {
            _hubService = hubService;
        }

        public async Task SendMessage(IModule module, params object[] value)
        {
            if (_hubService != null)
                await _hubService.SendMessage(module.Name, value);


        }
    }
}
