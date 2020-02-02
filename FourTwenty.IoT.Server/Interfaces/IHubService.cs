using System.Threading.Tasks;

namespace FourTwenty.IoT.Server.Interfaces
{
    public interface IHubService
    {
        Task SendMessage(string key, params object[] value);
    }
}
