using FourTwenty.IoT.Connect.Constants;

namespace FourTwenty.IoT.Connect.Data
{
    public class RelayData : BaseData
    {
        public int Pin { get; set; }
        public RelayState State { get; set; }
        public override string Value => $"#{Pin}={State}";

        public RelayData(int pin, RelayState state)
        {
            Pin = pin;
            State = state;
        }
    }

}
