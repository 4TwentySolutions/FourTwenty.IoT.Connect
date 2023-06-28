namespace FourTwenty.IoT.Connect.Data
{
    public class PinValueData : BaseData
    {
        public int PinValue { get; set; }

        public PinValueData(int pinValue)
        {
            PinValue = pinValue;
            Value = pinValue.ToString();
        }
    }

}
