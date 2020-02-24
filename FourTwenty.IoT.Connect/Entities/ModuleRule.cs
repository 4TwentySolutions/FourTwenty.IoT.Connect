using FourTwenty.Core.Data.Models;
using FourTwenty.IoT.Connect.Constants;
using System;

namespace FourTwenty.IoT.Connect.Entities
{
    public class ModuleRule : BaseEntity<int>
    {
        public RuleType RuleType { get; set; }
        /// <summary>
        /// Could be complex type (json , separated string's etc)
        /// </summary>
        public string RuleContent { get; set; }
        public Guid GrowBoxModuleId { get; set; }
        public virtual GrowBoxModule GrowBoxModule { get; set; }
    }
}
