using System.Collections.Generic;
using FourTwenty.IoT.Connect.Entities;

namespace FourTwenty.IoT.Server.ViewModels
{
    public class GrowBoxViewModel : EntityViewModel<GrowBox>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public int? Port { get; set; }
        public long TelegramBotChatId { get; set; }
        public long ConfigVersion { get; set; }
        public string PortStr
        {
            get => Port.ToString();
            set
            {
                if (string.IsNullOrEmpty(value))
                    Port = null;
                if (int.TryParse(value, out var port))
                    Port = port;
            }
        }
        public string TelegramBotChatIdSrt
        {
            get => TelegramBotChatId.ToString();
            set
            {
                if (string.IsNullOrEmpty(value))
                    TelegramBotChatId = 0;
                if (long.TryParse(value, out var chatId))
                    TelegramBotChatId = chatId;
            }
        }

        public ICollection<ModuleVm> Modules { get; set; }
    }
}
