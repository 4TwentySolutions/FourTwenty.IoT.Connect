using System.Collections.Generic;

namespace FourTwenty.IoT.Connect.Entities
{
    public class GrowBox : BaseEntity<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public int Port { get; set; }
        public long ConfigVersion { get; set; }
        public virtual ICollection<GrowBoxModule> Modules { get; set; }
    }
}
