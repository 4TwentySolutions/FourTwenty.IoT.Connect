using FourTwenty.Core.Data.Models;
using FourTwenty.IoT.Connect.Constants;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FourTwenty.IoT.Connect.Entities
{
    public class ModuleRule : BaseEntity<int>
    {
        public RuleType RuleType { get; set; }

        /// <summary>
        /// Could be complex type (json , separated string's etc)
        /// </summary>
        public string RuleContent { get; set; }
        public int ModuleId { get; set; }
        public int? Pin { get; set; }
        public bool IsEnabled { get; set; }
        public int? SortOrder { get; set; }
        public long? Delay { get; set; }

        [JsonIgnore]
        public virtual GrowBoxModule GrowBoxModule { get; set; }

        [JsonIgnore]
        public virtual ICollection<ModuleHistoryItem> ModuleHistoryItems { get; set; }
    }
}
