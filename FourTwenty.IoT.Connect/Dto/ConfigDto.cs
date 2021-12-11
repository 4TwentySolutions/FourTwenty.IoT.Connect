using System.Collections.Generic;
using FourTwenty.IoT.Connect.Models;

namespace FourTwenty.IoT.Connect.Dto
{
    public class ConfigDto
    {
        /// <summary>
        /// Config version - DateTime.Now.Ticks
        /// </summary>
        public long CurrentVersion { get; set; }


        /// <summary>
        /// Json model of GrowBoxViewModel
        /// </summary>
        public string Config { get; set; }
    }
}
