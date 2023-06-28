using FourTwenty.IoT.Connect.Interfaces;
using Newtonsoft.Json.Linq;

namespace FourTwenty.IoT.Connect.Data
{
    public class DhtData : TempData
    {
        public DhtData(double temp, double humidity) : base(temp)
        {
            Temperature = temp;
            Humidity = humidity;
            Value = ToString();
        }

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
