using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FourTwenty.Core.Data.Models;
using FourTwenty.IoT.Connect.Constants;
using Newtonsoft.Json;

namespace FourTwenty.IoT.Connect.Entities
{
    public class GrowBoxModule : BaseEntity<int>
    {
        [Required]
        public string Name { get; set; }
        public ComponentType ComponentType { get; set; }
        public int[] Pins { get; set; }
        public string PinsNames { get; set; }
        public string AdditionalData { get; set; }
        public int GrowBoxId { get; set; }
        
        [JsonIgnore]
        public virtual GrowBox GrowBox { get; set; }

        public virtual ICollection<ModuleRule> Rules { get; set; }
    }
}
