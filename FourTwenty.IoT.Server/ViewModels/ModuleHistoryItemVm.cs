using FourTwenty.IoT.Connect.Entities;
using System;
using FourTwenty.IoT.Connect.Helpers;
using Newtonsoft.Json;

namespace FourTwenty.IoT.Server.ViewModels
{
    public class ModuleHistoryItemVm : EntityVm<ModuleHistoryItem>
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Timezone { get; set; }
        public string Data { get; set; }
        public bool IsSuccess { get; set; }
        public string DataType { get; set; }
        public string Error { get; set; }

        public int RuleId { get; set; }

        [JsonIgnore]
        public ModuleRuleVm Rule { get; set; }

        [JsonIgnore]
        public string DateText => Date.ElapsedTime();
    }
}
