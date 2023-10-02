namespace FourTwenty.IoT.Connect.Data
{
    public class RangeFinderData : BaseData
    {
        public RangeFinderData(double distance)
        {
            Distance = distance;
            Value = distance.ToString();
        }

        public double Distance { get; set; }
    }

}
