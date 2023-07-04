namespace FourTwenty.IoT.Connect.Data
{
    public class PhData : BaseData
    {
        public double PhValue { get; set; }

        public PhData() : base() { }

        public PhData(double phValue) : base() {
        
            PhValue = phValue;
            Value = $"{phValue}pH";
        }
    }
}
