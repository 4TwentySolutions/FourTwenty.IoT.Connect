namespace FourTwenty.IoT.Connect.Data
{
    public class TempData : BaseData
    {
        public TempData(double temperature)
        {
            Temperature = temperature;
            Value = temperature.ToString();
        }

        /// <summary>
        /// Temperature in celsius
        /// </summary>
        public double Temperature { get; set; }
    }

}
