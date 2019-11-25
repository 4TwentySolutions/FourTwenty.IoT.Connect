using System.Collections.Generic;

namespace FourTwenty.IoT.Connect.Models.Config
{
    public class ConfigModel
    {
        public IList<BaseModule> Modules { get; set; }
        public int ListeningPort { get; set; }
    }


}
