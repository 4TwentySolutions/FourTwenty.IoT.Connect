using System.Threading.Tasks;

namespace FourTwenty.IoT.Server.Interfaces
{
    public interface ITelegramBotService
    {
        Task InitBot(long chatId);
        Task<bool> SendMessage(string message);
    }
}
