using FourTwenty.IoT.Connect.Interfaces;
using System.Threading.Tasks;

namespace FourTwenty.IoT.Server.Interfaces
{
    public interface ITelegramBotService : IInitializable
    {
        Task<bool> SendMessage(string message);
    }
}
