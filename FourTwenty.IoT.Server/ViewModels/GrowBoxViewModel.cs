using System.Collections.Generic;
using FourTwenty.IoT.Connect.Entities;

namespace FourTwenty.IoT.Server.ViewModels
{
    public class GrowBoxViewModel : GrowBox
    {
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
    }
}
