namespace FourTwenty.IoT.Connect.Data
{
    public class SoilMoistureData : BaseData
    {
        public override string Value => $"{Moisture}%";
        public double Moisture { get; set; }

        public SoilMoistureData(double moisture)
        {
            Moisture = moisture;
        }
    }

}
