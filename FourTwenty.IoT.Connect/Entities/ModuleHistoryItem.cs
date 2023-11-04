using FourTwenty.Core.Data.Models;
using System;
using Newtonsoft.Json;

namespace FourTwenty.IoT.Connect.Entities
{
    public class ModuleHistoryItem : BaseEntity<int>
    {
        public DateTime Date { get; set; }
        public string Timezone { get; set; }
        public string Data { get; set; }
        public bool IsSuccess { get; set; }
        public string DataType { get; set; }
        public string Error { get; set; }

        public int RuleId { get; set; }

        [JsonIgnore]
        public ModuleRule Rule { get; set; }

    }
}
