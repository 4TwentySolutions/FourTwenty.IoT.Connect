using System.Threading.Tasks;

namespace FourTwenty.IoT.Connect.Interfaces
{
    public interface ISensor
    {
        ValueTask<object> GetData();
    }
}
