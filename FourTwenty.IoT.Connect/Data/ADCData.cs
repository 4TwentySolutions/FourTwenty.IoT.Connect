namespace FourTwenty.IoT.Connect.Data
{
    /// <summary>
    /// Model for reading baseData from MCP3008 by python
    /// </summary>
    public class ADCData
    {
        public int Channel { get; set; }
        public double Value { get; set; }
        public double Voltage { get; set; }
    }
}
