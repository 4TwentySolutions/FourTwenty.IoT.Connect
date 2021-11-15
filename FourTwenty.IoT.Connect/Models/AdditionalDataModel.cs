using FourTwenty.IoT.Connect.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace FourTwenty.IoT.Connect.Models
{
    public class AdditionalDataModel
    {
        public DhtType? DhtType { get; set; }
        public bool? CloseOnInit { get; set; }
        public SensorReadType? SensorReadType { get; set; }
    }

    public class PinNameItem
    {
        public int Pin { get; set; }
        public string Name { get; set; }
    }
}
