using System.Threading.Tasks;

namespace FourTwenty.IoT.Connect.Interfaces
{
    public interface ISensor
    {
    }
    public interface ISensor<T> : ISensor
    {
        ValueTask<T> GetData();
    }

}
