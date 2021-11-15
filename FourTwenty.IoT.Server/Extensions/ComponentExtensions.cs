using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Server.Components.Relays;
using FourTwenty.IoT.Server.Components.Sensors;

namespace FourTwenty.IoT.Server.Extensions
{
    public static class ComponentExtensions
    {
        public static DhtSensor SetDhtType(this DhtSensor sensor, DhtType dhtType)
        {
            sensor.DhtType = dhtType;
            return sensor;
        }

        public static Relay SetCloseOnInit(this Relay relay, bool closeOnInit)
        {
            relay.CloseOnInit = closeOnInit;
            return relay;
        }


    }
}
