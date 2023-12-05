using System;

namespace FourTwenty.IoT.Connect.Data
{
    public class TempData : BaseData
    {
        public override string Value => $"{Temperature}\u00b0C";
        /// <summary>
        /// Temperature in celsius
        /// </summary>
        public double Temperature { get; set; }

        public TempData() { }
        public TempData(double temperature)
        {
            Temperature = temperature;
        }
    }

}
