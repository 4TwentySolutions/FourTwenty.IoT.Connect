using FourTwenty.IoT.Connect.Constants;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FourTwenty.IoT.Connect.Entities;
using FourTwenty.IoT.Connect.Models;

namespace FourTwenty.IoT.Server.ViewModels
{
    public class ComponentVm : EntityVm<GrowBoxModule>
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ComponentType ComponentType { get; set; }
        public int[] Pins { get; set; }
        //public bool GroupedModule { get; set; }

        public List<PinNameItem> PinsNames { get; set; }
        public int GrowBoxId { get; set; }
        public string AdditionalData { get; set; }
        public GrowBoxVm GrowBox { get; set; }
        public IList<ModuleRuleVm> Rules { get; set; }
        public Dictionary<int, string> RelayValues { get; set; }

        public string CurrentValueString { get; set; }
        public string LastValueTime { get; set; }

        #region States

        //public WorkState State => IotComponent?.RulesWorkState ?? WorkState.Stopped;

        //public string StateIcon => State == WorkState.Running ? "far fa-pause-circle" :
        //    State == WorkState.Paused ? "far fa-play-circle" : State == WorkState.Stopped ? "far fa-play-circle" : string.Empty;

        //public string BadgeIcon => State == WorkState.Running ? "badge-success" :
        //    State == WorkState.Paused ? "badge-warning" :
        //    State == WorkState.Stopped ? "badge-danger" : "badge-secondary";

        #endregion

    }
}
