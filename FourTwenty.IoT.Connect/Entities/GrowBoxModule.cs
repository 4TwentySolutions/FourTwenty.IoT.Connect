using System.Collections.Generic;
using FourTwenty.IoT.Connect.Constants;

namespace FourTwenty.IoT.Connect.Entities
{
    public class GrowBoxModule : BaseEntity<int>
    {
        public string Name { get; set; }
        public ModuleType Type { get; set; }
        public virtual GrowBox GrowBox { get; set; }
        public int GrowBoxId { get; set; }
        public int? Pin { get; set; }
        public string Pins { get; set; }
        public virtual ICollection<ModuleRule> Rules { get; set; }
    }
}
