namespace FourTwenty.IoT.Connect.Data
{
    public class RangeFinderData : BaseData
    {
        public override string Value => Distance.ToString();
        public RangeFinderData(double distance)
        {
            Distance = distance;
        }

        public double Distance { get; set; }
    }

}
