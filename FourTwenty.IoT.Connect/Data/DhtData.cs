namespace FourTwenty.IoT.Connect.Data
{
    public class DhtData : TempData
    {
        public DhtData() : base() {}

        public DhtData(double temp, double humidity) : base(temp)
        {
            Temperature = temp;
            Humidity = humidity;
            Value = ToString();
        }

        public double Humidity { get; set; }
    }

}
