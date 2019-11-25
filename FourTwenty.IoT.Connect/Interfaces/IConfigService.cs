using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FourTwenty.IoT.Connect.Models.Config;

namespace FourTwenty.IoT.Connect.Interfaces
{
    public interface IConfigService
    {
        Task<ConfigModel> GetConfig();
    }
}
