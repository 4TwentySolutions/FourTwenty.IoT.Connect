using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FourTwenty.Core.Data.Models;
using FourTwenty.IoT.Connect.Constants;

namespace FourTwenty.IoT.Connect.Entities
{
    public class GrowBoxModule : BaseEntity<int>
    {
        [Required]
        public string Name { get; set; }
        public ModuleType Type { get; set; }
        public int[] Pins { get; set; }
        public string AdditionalData { get; set; }
        public int GrowBoxId { get; set; }
        public virtual GrowBox GrowBox { get; set; }
        public virtual ICollection<ModuleRule> Rules { get; set; }
    }
}
