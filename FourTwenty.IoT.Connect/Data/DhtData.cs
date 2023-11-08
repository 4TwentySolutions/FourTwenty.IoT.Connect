namespace FourTwenty.IoT.Connect.Data
{
    public class DhtData : TempData
    {
        public override string Value => $"{Temperature}\u00b0C {Humidity}%H";

        public DhtData() {}

        public DhtData(double temp, double humidity) : base(temp)
        {
            Temperature = temp;
            Humidity = humidity;
        }

        public double Humidity { get; set; }
    }

}
