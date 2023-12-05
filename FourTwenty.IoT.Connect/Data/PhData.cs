namespace FourTwenty.IoT.Connect.Data
{
    public class PhData : BaseData
    {
        public override string Value => $"{PhValue}pH";
        public double PhValue { get; set; }

        public PhData() { }

        public PhData(double phValue)
        {
            PhValue = phValue;
        }
    }
}
