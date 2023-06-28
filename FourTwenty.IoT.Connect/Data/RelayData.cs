using FourTwenty.IoT.Connect.Constants;

namespace FourTwenty.IoT.Connect.Data
{
    public class RelayData : BaseData
    {
        public RelayData(int pin, RelayState state)
        {
            Pin = pin;
            State = state;
            Value = $"#{pin}={state.ToString()}";
        }

        public int Pin { get; set; }

        public RelayState State { get; set; }

        public override string ToString()
        {
            return $"{nameof(Pin)}: {State}";
        }
    }

}
