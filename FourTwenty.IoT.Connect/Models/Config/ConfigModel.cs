using System.Collections.Generic;
using FourTwenty.IoT.Connect.Modules;

namespace FourTwenty.IoT.Connect.Models.Config
{
    public class ConfigModel
    {
        
        public List<BaseModule> Modules { get; set; }

        /// <summary>
        /// Port for connections (default port - 8001) 
        /// </summary>
        public int ListeningPort { get; set; } = 8001;
    }


}
