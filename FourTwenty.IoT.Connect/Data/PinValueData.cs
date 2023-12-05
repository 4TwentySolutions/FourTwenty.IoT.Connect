namespace FourTwenty.IoT.Connect.Data
{
    public class PinValueData : BaseData
    {
        public override string Value => PinValue.ToString();
        public int PinValue { get; set; }

        public PinValueData(int pinValue)
        {
            PinValue = pinValue;
        }
    }

}
