using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FourTwenty.IoT.Connect.Interfaces
{
    public interface ISensor<T> : IModule
    {
        Task<T> GetData();
    }

}
