using System.Collections.Generic;
using FourTwenty.IoT.Connect.Models;

namespace FourTwenty.IoT.Connect.Dto
{
    public class ConfigDto
    {
        public List<BaseModule> Modules { get; set; }

        /// <summary>
        /// Port for connections (default port - 5000) 
        /// </summary>
        public int ListeningPort { get; set; } = 5000;

        /// <summary>
        /// Config version - DateTime.Now.Ticks
        /// </summary>
        public long CurrentVersion { get; set; }
    }
}
