using FourTwenty.IoT.Connect.Constants;

namespace FourTwenty.IoT.Connect.Models
{
    public class AdditionalDataModel
    {
        public DhtType? DhtType { get; set; }
        public bool? CloseOnInit { get; set; }
        public SensorReadType? SensorReadType { get; set; }
        public int? AnalogSensorReadChannel { get; set; }
    }

    public class PinNameItem
    {
        public int Pin { get; set; }
        public string Name { get; set; }

        public PinNameItem() { }

        public PinNameItem(int pin, string name)
        {
            Pin = pin;
            Name = name;
        }
    }
}
