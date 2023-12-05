using System.Threading.Tasks;
using FourTwenty.IoT.Connect.Constants;

namespace FourTwenty.IoT.Connect.Interfaces.Rules
{
    public interface IAction
    {
        ComponentType SensorType { get; }
        ActionType ActionType { get; }
        IRuleData Data { get; }
        Task Execute(object value = null);
    }
}
