using System.Threading.Tasks;

namespace FourTwenty.IoT.Connect.Interfaces.Services
{
    public interface IInitializeService<in T>
    {
        bool IsInitialized { get; }
        ValueTask Initialize(T options);
    }
}
