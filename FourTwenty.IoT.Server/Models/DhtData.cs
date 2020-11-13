using FourTwenty.IoT.Connect.Models;

namespace FourTwenty.IoT.Server.Models
{
    public class DhtData : ModuleResponse
    {
        public DhtData() : base() { }

        public DhtData(bool isSuccess) : base(isSuccess) { }

        public DhtData(double temp, double humidity)
        {
            Temperature = temp;
            Humidity = humidity;
        }

        /// <summary>
        /// Temperature in celsius
        /// </summary>
        public double Temperature { get; set; }
        public double Humidity { get; set; }

        /// <summary>
        /// Return DHT sensor values:
        ///     - Temperature (celsius)
        ///     - Humidity
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{nameof(Temperature)}: {Temperature}\n{nameof(Humidity)}: {Humidity}";
        }
    }
}
