using System.Threading.Tasks;
using FourTwenty.IoT.Connect.Interfaces.Modules;

namespace GrowIoT.Interfaces.Sensors
{
    public interface IDefaultSensor : IModule
    {
    }

    public interface ISensor<T> : IDefaultSensor
    {
        Task<T> GetData();
    }
}
