using System.Threading.Tasks;

namespace FourTwenty.IoT.Connect.Interfaces
{
    public interface IInitializable
    {
        ValueTask Initialize();
    }
}
