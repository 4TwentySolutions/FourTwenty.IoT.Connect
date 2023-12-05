using System.Device.Gpio;
using FourTwenty.IoT.Connect.Constants;

namespace FourTwenty.IoT.Server.Extensions
{
    public static class EnumExtensions
    {
        public static RelayState GetState(this PinValue value) => value == PinValue.Low ? RelayState.Opened : RelayState.Closed;
    }
}
