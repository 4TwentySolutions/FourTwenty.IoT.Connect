using System;
using FourTwenty.IoT.Connect.Constants;

namespace FourTwenty.IoT.Connect.Models
{
    public class ComponentJobMessage
    {
        public int ComponentId { get; set; }
        public int RuleId { get; set; }
        public Commands Command { get; set; }
        public int? Pin { get; set; }
        public TimeSpan? Period { get; set; }
    }
}
